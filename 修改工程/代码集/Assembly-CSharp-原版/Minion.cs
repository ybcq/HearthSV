using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Character
{
	public Minion(MinionCard card)
	{
		this.Player = card.Player;
		this.Card = card;
		this.Card.Minion = this;
		this.BaseAttack = card.BaseAttack;
		this.AttackModifiers = card.AttackModifiers;
		this.CurrentHealth = card.CurrentHealth;
		this.BaseHealth = card.BaseHealth;
		this.HealthModifiers = card.HealthModifiers;
		this.CantAttack = card.CantAttack;
		this.CantAttackTaunt = card.CantAttackTaunt;
		this.CantAttackHeroes = card.CantAttackHeroes;
		this.HasCharge = card.HasCharge;
		this.HasCleave = card.HasCleave;
		this.HasDivineShield = card.HasDivineShield;
		this.HasFreeze = card.HasFreeze;
		this.HasPoison = card.HasPoison;
		this.HasSpellshield = card.HasSpellshield;
		this.HasTaunt = card.HasTaunt;
		this.HasWindfury = card.HasWindfury;
		this.IsEvasive = card.IsEvasive;
		this.IsInaccurate = card.IsInaccurate;
		this.IsImmune = card.IsImmune;
		this.IsStealth = card.IsStealth;
		this.SpellPower = card.SpellPower;
		this.CurrentArmor = 0;
		this.Mechanics = this.Card.Mechanics;
	}

	public override int CurrentAttack
	{
		get
		{
			int num = this.BaseAttack;
			foreach (Func<int, int> func in this.AttackModifiers)
			{
				num = func(num);
				if (num < 0)
				{
					num = 0;
				}
			}
			foreach (Func<int, int> func2 in this.AuraAttackModifiers)
			{
				num = func2(num);
				if (num < 0)
				{
					num = 0;
				}
			}
			return num;
		}
	}

	public override int MaxHealth
	{
		get
		{
			int num = this.BaseHealth;
			foreach (Func<int, int> func in this.HealthModifiers)
			{
				num = func(num);
				if (num <= 0)
				{
					num = 1;
				}
			}
			foreach (Func<int, int> func2 in this.AuraHealthModifiers)
			{
				num = func2(num);
				if (num <= 0)
				{
					num = 1;
				}
			}
			return num;
		}
	}

	public override IEnumerator Attack(Character target)
	{
		Debugger.LogMinion(this, "starting attack to " + target.GetName());
		this.IsStealth = false;
		if (this.Player.Enemy.Minions.Count > 0)
		{
			if (this.IsInaccurate && RNG.RandomBool())
			{
				List<Character> allCharacters = this.Player.Enemy.GetAllCharacters();
				allCharacters.Remove(target);
				target = RNG.RandomItemFrom<Character>(allCharacters);
				Debugger.LogMinion(this, "switched target to " + target.GetName() + " (Inaccurate)");
			}
			if (target.IsEvasive && RNG.RandomBool())
			{
				if (target is Minion)
				{
					MinionEvadeEvent minionEvadeEvent = new MinionEvadeEvent
					{
						Attacker = this,
						Minion = (target as Minion)
					};
					ActionQueue.Add(() => EventManager.Instance.MinionEvadeHandler.Fire(minionEvadeEvent));
				}
				else
				{
					HeroEvadeEvent heroEvadeEvent = new HeroEvadeEvent
					{
						Attacker = this,
						Hero = (target as Hero)
					};
					ActionQueue.Add(() => EventManager.Instance.MinionEvadeHandler.Fire(heroEvadeEvent));
				}
				List<Character> allCharacters2 = this.Player.Enemy.GetAllCharacters();
				allCharacters2.Remove(target);
				target = RNG.RandomItemFrom<Character>(allCharacters2);
				Debugger.LogMinion(this, "switched target to " + target.GetName() + " (target evasive)");
			}
		}
		MinionPreAttackEvent minionPreAttackEvent = new MinionPreAttackEvent
		{
			Minion = this,
			Target = target,
			DamageAmount = this.CurrentAttack
		};
		yield return EventManager.Instance.OnMinionPreAttack(minionPreAttackEvent);
		yield return this.Mechanics.OnPreAttack.Fire(minionPreAttackEvent);
		target = minionPreAttackEvent.Target;
		int attackerAttack = minionPreAttackEvent.DamageAmount;
		int targetAttack = target.CurrentAttack;
		if (minionPreAttackEvent.Status != PreStatus.Cancelled)
		{
			Debugger.LogMinion(this, "attacking " + target.GetName());
			this.CurrentTurnAttacks++;
			int previousSelfHealth = this.CurrentHealth;
			int previousTargetHealth = target.CurrentHealth;
			if (target.IsHero())
			{
				yield return target.Damage(this, attackerAttack);
				this.Controller.AnimateAttack(target, previousTargetHealth - target.CurrentHealth, previousSelfHealth - this.CurrentHealth);
				ActionQueue.Add(new Func<IEnumerator>(target.CheckDeath));
			}
			else if (target.IsMinion())
			{
				Minion targetMinion = target.As<Minion>();
				if (this.IsAlive() && target.IsAlive())
				{
					bool canSelfPoison = !this.HasDivineShield && !this.IsImmune;
					bool canTargetPoison = !target.HasDivineShield && !target.IsImmune;
					yield return target.Damage(this, attackerAttack);
					yield return this.Damage(target, targetAttack);
					this.Controller.AnimateAttack(target, previousTargetHealth - target.CurrentHealth, previousSelfHealth - this.CurrentHealth);
					ActionQueue.Add(new Func<IEnumerator>(target.CheckDeath));
					ActionQueue.Add(new Func<IEnumerator>(this.CheckDeath));
					if (this.HasPoison && canTargetPoison && attackerAttack > 0)
					{
						Debugger.LogMinion(targetMinion, "killed by posion of " + this.GetName());
						yield return EventManager.Instance.OnMinionPoisoned(targetMinion, this);
						yield return targetMinion.Destroy();
					}
					if (targetMinion.HasPoison && canSelfPoison && targetAttack > 0)
					{
						Debugger.LogMinion(this, "killed by posion of " + targetMinion.GetName());
						yield return EventManager.Instance.OnMinionPoisoned(this, targetMinion);
						yield return this.Destroy();
					}
					if (this.HasFreeze)
					{
						target.Freeze();
					}
					if (target.HasFreeze)
					{
						base.Freeze();
					}
					if (this.HasCleave && attackerAttack > 0)
					{
						foreach (Minion minion in this.Player.Enemy.Minions)
						{
							if (targetMinion.IsDeadNextTo(minion))
							{
								yield return minion.Damage(this, attackerAttack);
								ActionQueue.Add(new Func<IEnumerator>(minion.CheckDeath));
								if (this.HasFreeze)
								{
									minion.Freeze();
								}
							}
						}
					}
				}
			}
			MinionAttackedEvent minionAttackedEvent = new MinionAttackedEvent
			{
				Minion = this,
				Target = target
			};
			yield return EventManager.Instance.OnMinionAttacked(minionAttackedEvent);
			AttackedEvent attackedEvent = new AttackedEvent
			{
				Damage = previousTargetHealth - target.CurrentHealth,
				Target = target
			};
			yield return this.Mechanics.OnAttacked.Fire(attackedEvent);
		}
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		yield break;
	}

	public override IEnumerator Heal(int healAmount)
	{
		Debugger.LogMinion(this, "healing for " + healAmount);
		MinionPreHealEvent minionPreHealEvent = new MinionPreHealEvent
		{
			Minion = this,
			HealAmount = healAmount
		};
		yield return EventManager.Instance.OnMinionPreHeal(minionPreHealEvent);
		if (minionPreHealEvent.Status != PreStatus.Cancelled)
		{
			this.CurrentHealth = Mathf.Min(this.CurrentHealth + minionPreHealEvent.HealAmount, this.MaxHealth);
			yield return EventManager.Instance.OnMinionHealed(this, minionPreHealEvent.HealAmount);
			if (minionPreHealEvent.HealAmount > 0)
			{
				InterfaceManager.Instance.SpawnHealSplatOn(this.Controller, minionPreHealEvent.HealAmount);
			}
			yield return this.CheckEnrage();
		}
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public override IEnumerator Damage(Character attacker, int damageAmount)
	{
		if (damageAmount < 0)
		{
			damageAmount = 0;
		}
		MinionPreDamageEvent minionPreDamageEvent = new MinionPreDamageEvent
		{
			Attacker = attacker,
			Minion = this,
			DamageAmount = damageAmount
		};
		if (this.HasDivineShield || this.IsImmune)
		{
			minionPreDamageEvent.DamageAmount = 0;
		}
		yield return EventManager.Instance.OnMinionPreDamage(minionPreDamageEvent);
		yield return this.Mechanics.OnPreDamage.Fire(minionPreDamageEvent);
		Debugger.LogMinion(this, string.Concat(new object[]
		{
			"receiving ",
			minionPreDamageEvent.DamageAmount,
			" damage by ",
			attacker.GetName()
		}));
		if (minionPreDamageEvent.Status != PreStatus.Cancelled)
		{
			this.HasDivineShield = false;
			this.CurrentHealth -= minionPreDamageEvent.DamageAmount;
			MinionDamagedEvent minionDamagedEvent = new MinionDamagedEvent
			{
				Attacker = attacker,
				Minion = this,
				DamageAmount = minionPreDamageEvent.DamageAmount
			};
			ActionQueue.Add(() => this.Mechanics.OnDamaged.Fire(minionDamagedEvent));
			ActionQueue.Add(() => EventManager.Instance.OnMinionDamaged(minionDamagedEvent));
			ActionQueue.Add(new Func<IEnumerator>(this.CheckEnrage));
		}
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		yield break;
	}

	public IEnumerator CheckEnrage()
	{
		if (this.CurrentHealth != this.MaxHealth)
		{
			yield return this.Mechanics.OnEnraged.Fire(this);
		}
		else
		{
			yield return this.Mechanics.OnDisenraged.Fire(this);
		}
		yield break;
	}

	public override IEnumerator CheckDeath()
	{
		if (!this.IsAlive())
		{
			Debugger.LogMinion(this, "died (check)");
			yield return this.Die();
		}
		yield break;
	}

	public IEnumerator Die()
	{
		if (!this.Player.DeadMinions.Contains(this.Card))
		{
			Debugger.LogMinion(this, "died");
			SoundManager.Instance.PlayMinionSound(this.Card, "Death", 0.5f);
			yield return this.Mechanics.Deathrattle.Fire(this);
			yield return EventManager.Instance.OnMinionDied(this);
			this.RemoveAuras();
			AuraManager.Instance.UpdateAuras();
			ActionQueue.StartParallel(new Func<IEnumerator>(this.Remove));
			this.Player.DeadMinions.Add(this.Card);
			GameManager.Instance.CurrentTurnDeadMinions++;
		}
		else
		{
			Debugger.LogMinion(this, " is already in the dead minions list (ERROR)");
		}
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		yield break;
	}

	public IEnumerator Destroy()
	{
		Debugger.LogMinion(this, "destroyed");
		yield return this.Mechanics.Deathrattle.Fire(this);
		yield return EventManager.Instance.OnMinionDied(this);
		this.RemoveAuras();
		AuraManager.Instance.UpdateAuras();
		ActionQueue.StartParallel(new Func<IEnumerator>(this.Remove));
		this.Player.DeadMinions.Add(this.Card);
		GameManager.Instance.CurrentTurnDeadMinions++;
		SoundManager.Instance.PlayMinionSound(this.Card, "Death", 0.5f);
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		yield break;
	}

	private IEnumerator Remove()
	{
		Debugger.LogMinion(this, "removed");
		yield return new WaitForSeconds(0.5f);
		yield return this.Controller.DestroyAnimation();
		this.Player.Minions.Remove(this);
		this.Player.BoardController.RemoveMinion(this);
		this.Controller.DestroyController();
		yield break;
	}

	private IEnumerator RemoveInanimated()
	{
		Debugger.LogMinion(this, "removed (inanimated)");
		yield return new WaitForSeconds(0.5f);
		this.Player.Minions.Remove(this);
		this.Player.BoardController.RemoveMinion(this);
		this.Controller.DestroyController();
		yield break;
	}

	public IEnumerator ReturnToHand()
	{
		Debugger.LogMinion(this, "returns to Hand");
		yield return this.RemoveInanimated();
		yield return this.Player.AddCardToHand(this.Card);
		yield break;
	}

	public IEnumerator ReturnToEnemyHand()
	{
		Debugger.LogMinion(this, "returns to enemy Hand");
		yield return this.RemoveInanimated();
		yield return this.Player.Enemy.AddCardToHand(this.Card);
		yield break;
	}

	public IEnumerator ReturnToDeck()
	{
		Debugger.LogMinion(this, "returns to Deck");
		yield return this.RemoveInanimated();
		this.Player.AddCardToDeck(this.Card);
		yield break;
	}

	public void Poison()
	{
		Debugger.LogCharacter(this, "now has poison");
		this.HasPoison = true;
		GameManager.Instance.GameUpdate();
	}

	public void Silence()
	{
		Debugger.LogMinion(this, "silenced");
		SoundManager.Instance.Play("Game_Mechanic_Silence");
		this.Mechanics.RemoveAll();
		this.AttackModifiers.Clear();
		this.HealthModifiers.Clear();
		if (this.CurrentHealth > this.MaxHealth)
		{
			this.CurrentHealth = this.MaxHealth;
		}
		this.HasTaunt = false;
		this.HasCharge = false;
		this.HasPoison = false;
		this.HasWindfury = false;
		this.HasDivineShield = false;
		this.IsImmune = false;
		this.HasSpellshield = false;
		this.IsStealth = false;
		this.SpellPower = 0;
		this.IsFrozen = false;
		this.UnfreezeNextTurn = false;
		this.IsInaccurate = false;
		this.IsSilenced = true;
		this.RemoveAuras();
		GameManager.Instance.GameUpdate();
	}

	public void RemoveAuras()
	{
		if (this.Card.MinionAura != null)
		{
			AuraManager.Instance.RemoveMinionAura(this.Card.MinionAura);
		}
		if (this.Card.CardAura != null)
		{
			AuraManager.Instance.RemoveCardAura(this.Card.CardAura);
		}
		if (this.Card.HeroPowerAura != null)
		{
			AuraManager.Instance.RemoveHeroPowerAura(this.Card.HeroPowerAura);
		}
		if (this.Card.HeroAura != null)
		{
			AuraManager.Instance.RemoveHeroAura(this.Card.HeroAura);
		}
		this.Card.MinionAura = null;
		this.Card.CardAura = null;
		this.Card.HeroPowerAura = null;
		this.Card.HeroAura = null;
		GameManager.Instance.GameUpdate();
	}

	public void TransformInto(MinionCard minionCard)
	{
		Debugger.LogMinion(this, "transforming into " + minionCard.Name);
		minionCard.SetOwner(this.Player);
		MinionController minionController = (MinionController)this.Controller;
		Minion minion = new Minion(minionCard);
		minion.Controller = minionController;
		minionController.Minion = minion;
		minionController.HoverController.Card = minionCard;
		minion.Controller.UpdateSprites();
		minion.Controller.UpdateNumbers();
		minionController.HoverController.UpdateNumbers();
		minionController.HoverController.UpdateSprites();
		int index = this.Player.Minions.IndexOf(this);
		this.Player.Minions[index] = minion;
	}

	public int GetPosition()
	{
		return this.Player.BoardController.GetPositionOf((MinionController)this.Controller);
	}

	public override bool IsMinion()
	{
		return true;
	}

	public bool IsNextTo(Minion other)
	{
		if (this.IsFriendlyOf(other) && this.IsAlive() && other.IsAlive())
		{
			int value = this.GetPosition() - other.GetPosition();
			if (Math.Abs(value) == 1)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsDeadNextTo(Minion other)
	{
		if (this.IsFriendlyOf(other))
		{
			int value = this.GetPosition() - other.GetPosition();
			if (Math.Abs(value) == 1)
			{
				return true;
			}
		}
		return false;
	}

	public override bool CanAttack()
	{
		if (this.CurrentAttack <= 0 || this.CantAttack || this.IsFrozen || (this.IsSleeping && !this.HasCharge))
		{
			return false;
		}
		if (this.HasWindfury)
		{
			return this.CurrentTurnAttacks < 2;
		}
		return this.CurrentTurnAttacks < 1;
	}

	public MinionCard Card;

	public Mechanics Mechanics;
}
