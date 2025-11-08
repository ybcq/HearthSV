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
				"加里维克斯的幸运币",
				() => this.Player.Hand.Any((BaseCard c) => c.CurrentCost == this.Player.AvailableMana + 1)
			},
			{
				"持斧指挥官",
				() => this.Player.HasWeapon()
			},
			{
				"亡者大军",
				() => this.Player.Minions.Count <= 3
			},
			{
				"泰伦·戈尔",
				() => this.Player.Minions.Count <= 4
			},
			{
				"达里安·莫格莱恩",
				() => !this.Player.HasWeapon()
			},
			{
				"鼓舞",
				() => this.Player.Hand.Any((BaseCard c) => c is MinionCard && c.CurrentCost == this.Player.AvailableMana + 1)
			},
			{
				"魔法茶",
				() => this.Player.Hand.Count((BaseCard c) => c is SpellCard && c.CurrentCost > 0) > 2
			},
			{
				"纺火花",
				() => this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Count > 0
			},
			{
				"旋转鹤踢",
				() => this.Player.Enemy.Minions.Count >= 2
			},
			{
				"翡翠玉龙",
				() => this.Player.Hero.CurrentHealth < this.Player.Enemy.Hero.CurrentHealth
			},
			{
				"土火风暴",
				() => this.Player.Minions.Count <= 4
			},
			{
				"虎之姿态",
				() => this.Player.Hero.CurrentAttack > 0
			}
		};
		this.SpecialTargetRules = new Dictionary<string, Func<List<Character>, Character>>
		{
			{
				"众生臣服",
				new Func<List<Character>, Character>(this.AllWillServeTargetRule)
			},
			{
				"凋零埋葬",
				new Func<List<Character>, Character>(this.DeathGripTargetRule)
			},
			{
				"战斗姿态",
				new Func<List<Character>, Character>(this.BrawlingStanceTargetRule)
			},
			{
				"奇波",
				new Func<List<Character>, Character>(this.ChiWaveTargetRule)
			},
			{
				"蛇之馈赠",
				new Func<List<Character>, Character>(this.GiftoftheSerpentTargetRule)
			},
			{
				"纺火花",
				new Func<List<Character>, Character>(this.SpinningFireBlossomTargetRule)
			},
			{
				"美汁源",
				new Func<List<Character>, Character>(this.TastyBrewTargetRule)
			}
		};
		this.SpecialComboConditions = new Dictionary<string, Func<bool>>
		{
			{
				"尸爆",
				new Func<bool>(this.CorpseExplosionComboCondition)
			}
		};
		this.SpecialCombos = new Dictionary<string, Func<IEnumerator>>
		{
			{
				"尸爆",
				new Func<IEnumerator>(this.CorpseExplosionCombo)
			}
		};
		this.SpecialPositionRules = new Dictionary<string, PositionPriority>
		{
			{
				"恐狼先锋",
				PositionPriority.Middle
			}
		};
		this.SpecialOrderRules = new Dictionary<string, OrderPriority>
		{
			{
				"奥术元素",
				OrderPriority.First
			},
			{
				"蜡烛",
				OrderPriority.First
			},
			{
				"硬币",
				OrderPriority.First
			},
			{
				"地精商人",
				OrderPriority.BeforeHeroPower
			},
			{
				"腐肉巨像",
				OrderPriority.First
			},
			{
				"乌木之刃辩护者",
				OrderPriority.First
			},
			{
				"尸爆",
				OrderPriority.First
			},
			{
				"泰伦·戈尔",
				OrderPriority.First
			},
			{
				"凋零埋葬",
				OrderPriority.First
			},
			{
				"持斧指挥官",
				OrderPriority.BeforeAttacking
			},
			{
				"迷魂之刃",
				OrderPriority.AfterAttacking
			},
			{
				"邪恶之刃",
				OrderPriority.AfterAttacking
			},
			{
				"达里安·莫格莱恩",
				OrderPriority.AfterAttacking
			},
			{
				"亡者大军",
				OrderPriority.BeforeHeroPower
			},
			{
				"嗜血伏击者",
				OrderPriority.Last
			},
			{
				"死亡凋零",
				OrderPriority.Last
			},
			{
				"鼓舞",
				OrderPriority.First
			},
			{
				"魔法茶",
				OrderPriority.First
			},
			{
				"翡翠玉龙",
				OrderPriority.First
			},
			{
				"戳",
				OrderPriority.BeforeAttacking
			},
			{
				"战斗姿态",
				OrderPriority.BeforeAttacking
			},
			{
				"有志之徒",
				OrderPriority.BeforeAttacking
			},
			{
				"美汁源",
				OrderPriority.BeforeAttacking
			},
			{
				"丽丽的水龙",
				OrderPriority.BeforeAttacking
			},
			{
				"旋转鹤踢",
				OrderPriority.BeforeAttacking
			},
			{
				"白虎雪恩",
				OrderPriority.BeforeAttacking
			},
			{
				"塔兰竹",
				OrderPriority.AfterAttacking
			},
			{
				"土火风暴",
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
				"沉默",
				OrderPriority.First
			},
			{
				"刺穿脊柱",
				OrderPriority.First
			}
		};
	}

	public override IEnumerator Think()
	{
		yield return new WaitForSeconds(1f);
		yield return this.CheckLethal();
		yield return new WaitForSeconds(0.5f);
		yield return base.PlayCardsWith(OrderPriority.First);
		yield return new WaitForSeconds(0.5f);
		yield return base.PlayCardsWith(OrderPriority.BeforeAttacking);
		yield return new WaitForSeconds(0.5f);
		yield return this.HeroAttack();
		yield return new WaitForSeconds(0.5f);
		yield return this.MinionAttack();
		yield return new WaitForSeconds(0.5f);
		yield return base.PlayCardsWith(OrderPriority.AfterAttacking);
		yield return new WaitForSeconds(0.5f);
		yield return base.PlayUnorderedCards();
		yield return new WaitForSeconds(0.5f);
		yield return base.PlayCardsWith(OrderPriority.BeforeHeroPower);
		yield return new WaitForSeconds(0.5f);
		yield return this.HeroPower();
		yield return new WaitForSeconds(0.5f);
		yield return base.PlayCardsWith(OrderPriority.AfterHeroPower);
		yield return new WaitForSeconds(0.5f);
		yield return this.HeroAttack();
		yield return new WaitForSeconds(0.5f);
		yield return this.MinionAttack();
		yield return new WaitForSeconds(0.5f);
		yield return base.PlayCardsWith(OrderPriority.Last);
		yield return new WaitForSeconds(0.5f);
		yield return GameManager.Instance.TurnEnd();
		yield break;
	}

	private IEnumerator CheckLethal()
	{
		if ((from m in this.Player.Enemy.Minions
		where m.HasTaunt && !m.IsStealth && m.IsAlive()
		select m).ToList<Minion>().Count == 0 && !this.Player.Enemy.Hero.IsImmune)
		{
			int num = 0;
			foreach (Minion minion2 in this.Player.Minions)
			{
				if (minion2.CanAttack() && minion2.CanAttackTo(this.Player.Enemy.Hero))
				{
					num += minion2.CurrentAttack;
					if (minion2.HasWindfury)
					{
						num += minion2.CurrentAttack;
					}
				}
			}
			if (this.Player.HasWeapon() && this.Player.Hero.CanAttack() && this.Player.Hero.CanAttackTo(this.Player.Enemy.Hero))
			{
				num += this.Player.Weapon.CurrentAttack;
				if (this.Player.Hero.HasWindfury)
				{
					num += this.Player.Weapon.CurrentAttack;
				}
			}
			if (num >= this.Player.Enemy.Hero.CurrentHealth)
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
					minion = null;
				}
				List<Minion>.Enumerator enumerator2 = default(List<Minion>.Enumerator);
			}
		}
		yield break;
		yield break;
	}

	private IEnumerator HeroAttack()
	{
		if (this.Player.Hero.CanAttack() && !this.Player.Hero.IsStealth)
		{
			List<Minion> targeteableEnemyMinions = base.GetTargeteableEnemyMinions(this.Player.Hero);
			if (targeteableEnemyMinions.Count > 0)
			{
				Minion target = (from m in targeteableEnemyMinions
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
				target = null;
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
				List<Minion> targeteableEnemyMinions = base.GetTargeteableEnemyMinions(minion);
				if (targeteableEnemyMinions.Count > 0)
				{
					Minion minion2 = (from m in targeteableEnemyMinions
					where m.CurrentHealth <= minion.CurrentAttack || m.HasTaunt
					orderby m.CurrentAttack descending
					select m).FirstOrDefault<Minion>();
					if (minion2 != null)
					{
						yield return minion.Attack(minion2);
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
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		yield break;
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
		SpellCard spellCard = this.Player.Hand.First((BaseCard c) => c.Name == "Corpse Explosion") as SpellCard;
		Character ghoul = this.Player.Minions.Last((Minion m) => m.Card.Name == "Ghoul");
		yield return this.Player.PlaySpell(spellCard, ghoul);
		yield return new WaitForSeconds(1.5f);
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where m.CurrentHealth > 2 && ghoul.CanAttackTo(m)
		select m).ToList<Minion>();
		if (list.Count > 0)
		{
			ghoul.Attack((from m in list
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
