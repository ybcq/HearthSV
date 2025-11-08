using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : Character
{
	public override int CurrentAttack
	{
		get
		{
			int num = this.BaseAttack;
			if (this.Player.IsCurrent() && this.Player.HasWeapon())
			{
				num += this.Player.Weapon.CurrentAttack;
			}
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
		Debugger.LogHero(this, "starting attack to " + target.GetName());
		if (this.Player.Enemy.Minions.Count > 0)
		{
			if (this.IsInaccurate && RNG.RandomBool())
			{
				List<Character> allCharacters = this.Player.Enemy.GetAllCharacters();
				allCharacters.Remove(target);
				target = RNG.RandomItemFrom<Character>(allCharacters);
				Debugger.LogHero(this, "switched target to " + target.GetName() + " (Inaccurate)");
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
				Debugger.LogHero(this, "switched target to " + target.GetName() + " (target evasive)");
			}
		}
		this.IsStealth = false;
		HeroPreAttackEvent heroPreAttackEvent = new HeroPreAttackEvent
		{
			Hero = this,
			Target = target
		};
		yield return EventManager.Instance.OnHeroPreAttack(heroPreAttackEvent);
		target = heroPreAttackEvent.Target;
		if (heroPreAttackEvent.Status != PreStatus.Cancelled)
		{
			Debugger.LogHero(this, "attacking " + target.GetName());
			int previousSelfHealth = this.CurrentHealth;
			int previousTargetHealth = target.CurrentHealth;
			if (target.IsHero())
			{
				yield return target.Damage(this, this.CurrentAttack);
				this.Controller.AnimateAttack(target, previousTargetHealth - target.CurrentHealth, previousSelfHealth - this.CurrentHealth);
				ActionQueue.Add(new Func<IEnumerator>(target.CheckDeath));
			}
			else
			{
				yield return target.Damage(this, this.CurrentAttack);
				yield return this.Damage(target, target.CurrentAttack);
				this.Controller.AnimateAttack(target, previousTargetHealth - target.CurrentHealth, previousSelfHealth - this.CurrentHealth);
				ActionQueue.Add(new Func<IEnumerator>(target.CheckDeath));
				ActionQueue.Add(new Func<IEnumerator>(this.CheckDeath));
			}
			this.CurrentTurnAttacks++;
			if (this.Player.HasWeapon())
			{
				this.Player.Weapon.Use();
			}
			HeroController heroController = (HeroController)this.Controller;
			while (heroController.IsAnimating)
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.25f);
			yield return EventManager.Instance.OnHeroAttacked(this, target);
			if (this.HasFreeze)
			{
				target.Freeze();
			}
			if (target.HasFreeze)
			{
				this.Freeze();
			}
			if (this.Player.HasWeapon())
			{
				AttackedEvent attackedEvent = new AttackedEvent
				{
					Damage = previousTargetHealth - target.CurrentHealth,
					Target = target
				};
				yield return this.Player.Weapon.Card.Mechanics.OnAttacked.Fire(attackedEvent);
			}
		}
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		yield break;
	}

	public override IEnumerator Damage(Character attacker, int damageAmount)
	{
		HeroPreDamageEvent heroPreDamageEvent = new HeroPreDamageEvent
		{
			Hero = this,
			Attacker = attacker,
			DamageAmount = damageAmount
		};
		yield return EventManager.Instance.OnHeroPreDamage(heroPreDamageEvent);
		Debugger.LogHero(this, string.Concat(new object[]
		{
			"receiving ",
			heroPreDamageEvent.DamageAmount,
			" damage by ",
			attacker.GetName()
		}));
		if (heroPreDamageEvent.Status != PreStatus.Cancelled)
		{
			this.Damage(heroPreDamageEvent.DamageAmount);
			ActionQueue.Add(() => EventManager.Instance.OnHeroDamaged(this.$this, attacker, heroPreDamageEvent.DamageAmount));
		}
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		yield break;
	}

	public override IEnumerator Heal(int healAmount)
	{
		HeroPreHealEvent heroPreHealEvent = new HeroPreHealEvent
		{
			Hero = this,
			HealAmount = healAmount
		};
		yield return EventManager.Instance.OnHeroPreHeal(heroPreHealEvent);
		if (heroPreHealEvent.Status != PreStatus.Cancelled)
		{
			Debugger.LogHero(this, "healing for " + heroPreHealEvent.HealAmount);
			this.CurrentHealth = Mathf.Min(this.CurrentHealth + heroPreHealEvent.HealAmount, this.MaxHealth);
			yield return EventManager.Instance.OnHeroHealed(this, heroPreHealEvent.HealAmount);
			if (heroPreHealEvent.HealAmount > 0)
			{
				InterfaceManager.Instance.SpawnHealSplatOn(this.Controller, heroPreHealEvent.HealAmount);
			}
		}
		GameManager.Instance.GameUpdate();
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public override IEnumerator CheckDeath()
	{
		if (!this.IsAlive())
		{
			Debugger.LogHero(this, "died (check)");
			Debugger.Log("GAME ENDED");
			yield return new WaitForSeconds(0.25f);
			GameManager.Instance.EndGame(this.Player);
		}
		yield break;
	}

	private void Damage(int damageAmount)
	{
		if (this.CurrentArmor > 0)
		{
			if (damageAmount >= this.CurrentArmor)
			{
				damageAmount -= this.CurrentArmor;
				this.CurrentArmor = 0;
			}
			else
			{
				this.CurrentArmor -= damageAmount;
				damageAmount = 0;
			}
		}
		this.CurrentHealth -= damageAmount;
	}

	public new void Freeze()
	{
		Debugger.LogHero(this, "frozen");
		SoundManager.Instance.Play("Game_Mechanic_Freeze");
		this.IsFrozen = true;
		this.UnfreezeNextTurn = false;
		GameManager.Instance.GameUpdate();
	}

	public abstract BaseHeroPower GetDefaultHeroPower();

	public override bool IsHero()
	{
		return true;
	}

	public override bool CanAttack()
	{
		if (!this.IsFrozen)
		{
			if (this.Player.HasWeapon() && this.Player.Weapon.CurrentAttack > 0)
			{
				if (this.Player.Weapon.HasWindfury || this.HasWindfury)
				{
					return this.CurrentTurnAttacks < 2;
				}
				return this.CurrentTurnAttacks == 0;
			}
			else if (this.CurrentAttack > 0)
			{
				if (this.HasWindfury)
				{
					return this.CurrentTurnAttacks < 2;
				}
				return this.CurrentTurnAttacks == 0;
			}
		}
		return false;
	}

	public BaseHeroPower HeroPower;

	public HeroClass Class;
}
