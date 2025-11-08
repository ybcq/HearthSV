using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleAI : BaseAI
{
	public SimpleAI()
	{
		this.SpecialPlayRules = new Dictionary<string, Func<bool>>
		{
			{
				"Coin",
				() => this.Player.Hand.Any((BaseCard c) => c.CurrentCost == this.Player.AvailableMana + 1)
			},
			{
				"Dancing Rune Weapon",
				() => this.Player.HasWeapon()
			},
			{
				"Army of the Dead",
				() => this.Player.Minions.Count <= 3
			},
			{
				"Teron Gorefiend",
				() => this.Player.Minions.Count <= 4
			},
			{
				"Darion Mograine",
				() => !this.Player.HasWeapon()
			},
			{
				"Ascension",
				() => this.Player.Hand.Any((BaseCard c) => c is MinionCard && c.CurrentCost == this.Player.AvailableMana + 1)
			},
			{
				"Mana Tea",
				() => this.Player.Hand.Count((BaseCard c) => c is SpellCard && c.CurrentCost > 0) > 2
			},
			{
				"Spinning Fire Blossom",
				() => this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Count > 0
			},
			{
				"Spinning Crane Kick",
				() => this.Player.Enemy.Minions.Count >= 2
			},
			{
				"Yu'lon the Jade Serpent",
				() => this.Player.Hero.CurrentHealth < this.Player.Enemy.Hero.CurrentHealth
			},
			{
				"Storm, Earth and Fire",
				() => this.Player.Minions.Count <= 4
			},
			{
				"Tiger Stance",
				() => this.Player.Hero.CurrentAttack > 0
			}
		};
		this.SpecialTargetRules = new Dictionary<string, Func<List<Character>, Character>>
		{
			{
				"All Will Serve",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"Blood Strike",
				new Func<List<Character>, Character>(this.BloodStrikeTargetRule)
			},
			{
				"Death Grip",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"Brawling Stance",
				new Func<List<Character>, Character>(this.BrawlingStanceTargetRule)
			},
			{
				"Chi Wave",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"Gift of the Serpent",
				new Func<List<Character>, Character>(this.GiftoftheSerpentTargetRule)
			},
			{
				"Spinning Fire Blossom",
				new Func<List<Character>, Character>(this.SpinningFireBlossomTargetRule)
			},
			{
				"Tasty Brew",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			}
		};
		this.SpecialComboConditions = new Dictionary<string, Func<bool>>
		{
			{
				"Corpse Explosion",
				new Func<bool>(this.CorpseExplosionComboCondition)
			}
		};
		this.SpecialCombos = new Dictionary<string, Func<IEnumerator>>
		{
			{
				"Corpse Explosion",
				new Func<IEnumerator>(this.CorpseExplosionCombo)
			}
		};
		this.SpecialPositionRules = new Dictionary<string, PositionPriority>
		{
			{
				"Elwynn Forest Wolf",
				PositionPriority.Middle
			}
		};
		this.SpecialOrderRules = new Dictionary<string, OrderPriority>
		{
			{
				"Arcaneling",
				OrderPriority.First
			},
			{
				"Candle",
				OrderPriority.First
			},
			{
				"Coin",
				OrderPriority.First
			},
			{
				"Goblin Merchant",
				OrderPriority.BeforeHeroPower
			},
			{
				"Flesh Colossus",
				OrderPriority.First
			},
			{
				"Ebon Blade Vindicator",
				OrderPriority.First
			},
			{
				"Corpse Explosion",
				OrderPriority.First
			},
			{
				"Teron Gorefiend",
				OrderPriority.First
			},
			{
				"Death Grip",
				OrderPriority.First
			},
			{
				"Dancing Rune Weapon",
				OrderPriority.BeforeAttacking
			},
			{
				"Blade of Lost Souls",
				OrderPriority.AfterAttacking
			},
			{
				"Unholy Runeblade",
				OrderPriority.AfterAttacking
			},
			{
				"Darion Mograine",
				OrderPriority.AfterAttacking
			},
			{
				"Army of the Dead",
				OrderPriority.BeforeHeroPower
			},
			{
				"Raise Dead",
				OrderPriority.Last
			},
			{
				"Death and Decay",
				OrderPriority.Last
			},
			{
				"Ascension",
				OrderPriority.First
			},
			{
				"Mana Tea",
				OrderPriority.First
			},
			{
				"Yu'lon the Jade Serpent",
				OrderPriority.First
			},
			{
				"Jab",
				OrderPriority.BeforeAttacking
			},
			{
				"Brawling Stance",
				OrderPriority.BeforeAttacking
			},
			{
				"Aspiring Student",
				OrderPriority.BeforeAttacking
			},
			{
				"Tasty Brew",
				OrderPriority.BeforeAttacking
			},
			{
				"Lili's Water Dragon",
				OrderPriority.BeforeAttacking
			},
			{
				"Spinning Crane Kick",
				OrderPriority.BeforeAttacking
			},
			{
				"Xuen, the White Tiger",
				OrderPriority.BeforeAttacking
			},
			{
				"Taran Zhu",
				OrderPriority.AfterAttacking
			},
			{
				"Storm, Earth and Fire",
				OrderPriority.BeforeHeroPower
			},
			{
				"Burning",
				OrderPriority.First
			},
			{
				"Chained",
				OrderPriority.First
			},
			{
				"Misery",
				OrderPriority.First
			},
			{
				"Silence",
				OrderPriority.First
			},
			{
				"Impaling Spine",
				OrderPriority.First
			}
		};
	}

	public override IEnumerator Think()
	{
		yield return new WaitForSeconds(1f);
		yield return this.CheckLethal();
		yield return base.PlayCardsWith(OrderPriority.First);
		yield return base.PlayCardsWith(OrderPriority.BeforeAttacking);
		yield return this.HeroAttack();
		yield return this.MinionAttack();
		yield return base.PlayCardsWith(OrderPriority.AfterAttacking);
		yield return base.PlayUnorderedCards();
		yield return base.PlayCardsWith(OrderPriority.BeforeHeroPower);
		yield return this.HeroPower();
		yield return base.PlayCardsWith(OrderPriority.AfterHeroPower);
		yield return this.HeroAttack();
		yield return this.MinionAttack();
		yield return base.PlayCardsWith(OrderPriority.Last);
		yield return GameManager.Instance.TurnEnd();
		yield break;
	}

	private IEnumerator CheckLethal()
	{
		List<Minion> tauntMinions = (from m in this.Player.Enemy.Minions
		where m.HasTaunt && !m.IsStealth && m.IsAlive()
		select m).ToList<Minion>();
		if (tauntMinions.Count == 0 && !this.Player.Enemy.Hero.IsImmune)
		{
			int totalDamage = 0;
			foreach (Minion minion2 in this.Player.Minions)
			{
				if (minion2.CanAttack() && minion2.CanAttackTo(this.Player.Enemy.Hero))
				{
					totalDamage += minion2.CurrentAttack;
					if (minion2.HasWindfury)
					{
						totalDamage += minion2.CurrentAttack;
					}
				}
			}
			if (this.Player.HasWeapon() && this.Player.Hero.CanAttack() && this.Player.Hero.CanAttackTo(this.Player.Enemy.Hero))
			{
				totalDamage += this.Player.Weapon.CurrentAttack;
				if (this.Player.Hero.HasWindfury)
				{
					totalDamage += this.Player.Weapon.CurrentAttack;
				}
			}
			if (totalDamage >= this.Player.Enemy.Hero.CurrentHealth)
			{
				if (this.Player.HasWeapon())
				{
					yield return this.Player.Hero.Controller.LevitateWaitAnimation();
					yield return this.Player.Hero.Attack(this.Player.Enemy.Hero);
				}
				foreach (Minion minion in this.Player.Minions)
				{
					if (minion.CanAttack() && minion.CanAttackTo(this.Player.Enemy.Hero))
					{
						yield return minion.Attack(this.Player.Enemy.Hero);
						if (minion.HasWindfury)
						{
							yield return minion.Attack(this.Player.Enemy.Hero);
						}
					}
				}
			}
		}
		yield break;
	}

	private IEnumerator HeroAttack()
	{
		if (this.Player.Hero.CanAttack() && !this.Player.Hero.IsStealth)
		{
			List<Minion> targeteableMinions = base.GetTargeteableEnemyMinions(this.Player.Hero);
			if (targeteableMinions.Count > 0)
			{
				Minion target = (from m in targeteableMinions
				where m.CurrentHealth <= this.Player.Hero.CurrentAttack || m.HasTaunt
				orderby m.CurrentAttack descending
				select m).FirstOrDefault<Minion>();
				if (target != null && target.CurrentAttack < this.Player.Hero.CurrentHealth)
				{
					yield return this.Player.Hero.Controller.LevitateWaitAnimation();
					yield return this.Player.Hero.Attack(target);
				}
				else if (this.Player.Hero.CanAttackTo(this.Player.Enemy.Hero))
				{
					yield return this.Player.Hero.Controller.LevitateWaitAnimation();
					yield return this.Player.Hero.Attack(this.Player.Enemy.Hero);
				}
			}
			else if (this.Player.Hero.CanAttackTo(this.Player.Enemy.Hero))
			{
				yield return this.Player.Hero.Controller.LevitateWaitAnimation();
				yield return this.Player.Hero.Attack(this.Player.Enemy.Hero);
			}
		}
		yield break;
	}

	private IEnumerator MinionAttack()
	{
		using (List<Minion>.Enumerator enumerator = (from m in this.Player.Minions
		where m.CanAttack() && m.IsAlive()
		orderby m.CurrentAttack
		select m).ToList<Minion>().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				List<Minion> targeteableMinions = base.GetTargeteableEnemyMinions(minion);
				if (targeteableMinions.Count > 0)
				{
					Minion target = (from m in targeteableMinions
					where m.CurrentHealth <= minion.CurrentAttack || m.HasTaunt
					orderby m.CurrentAttack descending
					select m).FirstOrDefault<Minion>();
					if (target != null)
					{
						yield return minion.Attack(target);
						yield return new WaitForSeconds(0.5f);
					}
					else if (minion.CanAttackTo(this.Player.Enemy.Hero))
					{
						yield return minion.Attack(this.Player.Enemy.Hero);
						yield return new WaitForSeconds(0.5f);
					}
				}
				else if (minion.CanAttackTo(this.Player.Enemy.Hero))
				{
					yield return minion.Attack(this.Player.Enemy.Hero);
					yield return new WaitForSeconds(0.5f);
				}
			}
		}
		yield break;
	}

	private IEnumerator HeroPower()
	{
		if (this.Player.Hero.HeroPower.IsAvailable())
		{
			string name = this.Player.Hero.HeroPower.Name;
			if (name != null)
			{
				if (!(name == "Raise Ghoul"))
				{
					if (name == "Chi Burst")
					{
						if (this.Player.Minions.Any((Minion m) => m.CurrentHealth < m.MaxHealth))
						{
							yield return this.Player.Hero.HeroPower.Controller.RotateDownAnimation();
							yield return this.Player.UseHeroPower((from m in this.Player.Minions
							orderby m.CurrentHealth
							select m).First<Minion>());
						}
						else if (this.Player.Hero.CurrentHealth < this.Player.Hero.MaxHealth)
						{
							yield return this.Player.Hero.HeroPower.Controller.RotateDownAnimation();
							yield return this.Player.UseHeroPower(this.Player.Hero);
						}
					}
				}
				else if (this.Player.Minions.Count < 7)
				{
					yield return this.Player.Hero.HeroPower.Controller.RotateDownAnimation();
					yield return this.Player.UseHeroPower(null);
				}
			}
		}
		yield break;
	}

	public Character ImpalingSpineTargetRule(List<Character> characters)
	{
		List<Character> list = (from c in characters
		where c is Minion && c.As<Minion>().Card is HighWarlordNajentus
		select c).ToList<Character>();
		if (list.Count > 0)
		{
			return RNG.RandomItemFrom<Character>(list);
		}
		return RNG.RandomItemFrom<Character>(characters);
	}

	public Character AllWillServeTargetRule(List<Character> characters)
	{
		List<Character> list = (from c in characters
		where c.CurrentHealth <= 2
		select c).ToList<Character>();
		if (list.Count > 0)
		{
			return (from c in list
			where this.Player.Hero.IsEnemyOf(c)
			orderby c.CurrentAttack descending
			select c).FirstOrDefault<Character>();
		}
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack descending
		select c).FirstOrDefault<Character>();
	}

	public Character BloodStrikeTargetRule(List<Character> characters)
	{
		List<Character> list = (from c in characters
		where c.CurrentHealth <= 4
		select c).ToList<Character>();
		if (list.Count > 0)
		{
			return (from c in list
			orderby c.CurrentAttack descending
			select c).FirstOrDefault<Character>();
		}
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack descending
		select c).FirstOrDefault<Character>();
	}

	public Character DeathGripTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack + c.CurrentHealth descending
		select c).FirstOrDefault<Character>();
	}

	public Character BrawlingStanceTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsFriendlyOf(c)
		orderby c.CurrentAttack
		select c).FirstOrDefault<Character>();
	}

	public Character GiftoftheSerpentTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentAttack + c.CurrentHealth descending
		select c).FirstOrDefault<Character>();
	}

	public Character ChiWaveTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsFriendlyOf(c)
		where c.CurrentHealth < c.MaxHealth
		orderby c.MaxHealth - c.CurrentHealth descending
		select c).FirstOrDefault<Character>();
	}

	public Character TastyBrewTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsFriendlyOf(c)
		orderby c.CurrentHealth
		select c).FirstOrDefault<Character>();
	}

	public Character SpinningFireBlossomTargetRule(List<Character> characters)
	{
		return (from c in characters
		where this.Player.Hero.IsEnemyOf(c)
		orderby c.CurrentHealth descending
		select c).FirstOrDefault<Character>();
	}

	public bool CorpseExplosionComboCondition()
	{
		return this.Player.AvailableMana >= 5 && this.Player.Hero.HeroPower.IsAvailable() && this.Player.Minions.Count < 7 && this.Player.Enemy.Minions.Count > 2;
	}

	public IEnumerator CorpseExplosionCombo()
	{
		yield return this.HeroPower();
		SpellCard corpseExplosion = this.Player.Hand.First((BaseCard c) => c.Name == "Corpse Explosion") as SpellCard;
		Character ghoul = this.Player.Minions.Last((Minion m) => m.Card.Name == "Ghoul");
		yield return this.Player.PlaySpell(corpseExplosion, ghoul);
		yield return new WaitForSeconds(1.5f);
		List<Minion> targets = (from m in this.Player.Enemy.Minions
		where m.CurrentHealth > 2 && ghoul.CanAttackTo(m)
		select m).ToList<Minion>();
		if (targets.Count > 0)
		{
			ghoul.Attack((from m in targets
			orderby m.CurrentAttack descending
			select m).First<Minion>());
		}
		else
		{
			ghoul.Attack(base.GetRandomEnemyMinion());
		}
		yield break;
	}
}
