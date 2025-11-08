using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//游戏机制动画
this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
yield return new WaitForSeconds(0.5f);

this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
yield return new WaitForSeconds(0.5f);

//攻击动画
InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Hero.Controller, 1);
InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, damage);
SpawnDamageSplatOn(HeroController controller, int damage)

//治疗动画
InterfaceManager.Instance.SpawnHealSplatOn(HeroController controller, int damage)

//法强
int damage = 5 + this.Player.GetSpellPower();

//手牌数
this.Player.Hand.Count

//本回合水晶数 
this.Player.TurnMana

//不能收藏
this.Collectible = false;




//抽取指定类型的牌
		if (self.Player.Deck.ContainsCardOfType<WeaponCard>())
		{
			List<BaseCard> cardsOfType = self.Player.Deck.GetCardsOfType<WeaponCard>();
			if (cardsOfType.Count > 0)
			{
				BaseCard card = RNG.RandomItemFrom<BaseCard>(cardsOfType);
				yield return self.Player.DrawFromDeck(card, null);
			}
		}
		yield break;

//随机摧毁
		Minion randomMinion = RNG.RandomItemFrom<Minion>((from m in this.Player.Minions
		where m.IsAlive()
		select m).ToList<Minion>());
		if (randomMinion != null)
		{
			yield return randomMinion.Destroy();
		}
		yield return new WaitForSeconds(0.25f);
		yield break;


//使一个随从获得突袭
	public override IEnumerator Use(Character target)
	{
		if (target.IsFriendlyOf(this.Hero))
		{
			target.As<Minion>().CantAttackHeroes = true;
			Minion targetMinion = (Minion)target;
      targetMinion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, targetMinion));
		}
		
		yield break;
	}
	
	public IEnumerator OnTurnEnd(TurnEvent evt, Minion self)
	{
		self.CantAttackHeroes = false;
		yield break;
	}

//禁用英雄技能
using System;
		this.HeroAura = new Aura<Hero>(new Action<Hero>(this.ApplyAura), new Action<Hero>(this.RemoveAura), new Func<Hero, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeSpell();
	}

	public void ApplyAura(Hero hero)
	{
		hero.Player.CanHeroPower = false;
	}

	public void RemoveAura(Hero hero)
	{
		hero.Player.CanHeroPower = true;
	}

	public bool ApplyCondition(Hero hero)
	{
		return hero.Player == this.Player;
	}

	public bool ExistCondition()
	{
		return this.Player.Hand.Contains(this);
	}
}


//使一个随从获得回合结束特效
public class Immolation : SpellCard
{
	public Immolation()
	{
		this.Name = "Immolation";
		this.Description = "Give a friendly minion \"At the end of your turn, deal 1 damage to all enemies.\"";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, targetMinion));
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent evt, Minion self)
	{
		if (evt.Player == this.Player)
		{
			self.IsStealth = false;
			List<Character> availableTargets = this.Player.Enemy.GetAllCharacters();
			foreach (Character enemy in availableTargets)
			{
				yield return enemy.Damage(null, 1);
			}
			foreach (Character enemy2 in availableTargets)
			{
				yield return enemy2.CheckDeath();
			}
		}
		yield break;
	}
}

//英雄类别
this.Player.Hero.Class

//沉默冻结
if (frozenGhoul.Minion != null)
{
  frozenGhoul.Minion.Silence();
  frozenGhoul.Minion.Freeze();
}

//所有随从操作
public override IEnumerator Cast(Character target)
{
  foreach (Minion minion in GameManager.Instance.GetAllMinions())
  {
    if (minion.IsFrozen)
    {
      yield return minion.Destroy();
    }
  }
  yield break;
}

//反映时间
yield return new WaitForSeconds(0.25f)

//基础的食尸鬼
ChargeTurnGhoul() 

//亡语召唤
this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));

	public IEnumerator Deathrattle(Minion self)
	{
		int teronPosition = this.Minion.GetPosition();
		if (teronPosition != -1)
		{
			yield return self.Player.SummonMinion(new GhostriderofKarabor(), teronPosition + 1);
			yield return self.Player.SummonMinion(new GhostriderofKarabor(), teronPosition);
		}
		else
		{
			yield return self.Player.SummonMinion(new GhostriderofKarabor());
			yield return self.Player.SummonMinion(new GhostriderofKarabor());
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}

//抽取武器
		if (self.Player.Deck.ContainsCardOfType<WeaponCard>())
		{
			List<BaseCard> weaponsInDeck = self.Player.Deck.GetCardsOfType<WeaponCard>();
			if (weaponsInDeck.Count > 0)
			{
				BaseCard randomWeapon = RNG.RandomItemFrom<BaseCard>(weaponsInDeck);
				yield return self.Player.DrawFromDeck(randomWeapon, null);
			}
		}
		
//双方英雄伤害
	public IEnumerator Deathrattle(Minion self)
	{
		yield return this.Player.Enemy.Hero.Damage(null, 5);
		yield return this.Player.Hero.Damage(null, 5);
		yield return this.Player.Enemy.Hero.CheckDeath();
		yield return this.Player.Hero.CheckDeath();
		yield break;
	}
	
//对敌方伤害或对友方治疗
	this.BattlecryType = BattlecryType.AllCharacters;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		if (target.IsEnemyOf(this.Minion))
		{
			yield return target.Damage(null, 3);
			yield return target.CheckDeath();
		}
		else
		{
			yield return target.Heal(6);
			yield return target.CheckDeath();
		}
		yield break;
	}
	
//回合开始时
	this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			Character randomTarget = RNG.RandomItemFrom<Character>(this.Player.Enemy.GetAllCharacters());
			yield return randomTarget.Damage(null, this.Minion.CurrentAttack);
			yield return randomTarget.CheckDeath();
		}
		yield break;
		yield break;
		
		yield return target.As<Minion>().Destroy();
		yield return this.Player.Hero.Heal(4);
		yield return this.Player.Draw(null);
	}


		this.Minion.HasCharge = true;
		yield break;

		foreach (Minion minion in player.Minions)
		{
			minion.AddAttackModifier(new Func<int, int>(this.MeditateModifier));
			minion.CurrentHealth++;
			minion.AddHealthModifier(new Func<int, int>(this.MeditateModifier));
		}
		yield break;

//所有随从获得+1/+1

	public int MeditateModifier(int value)
	{
		return value + 1;
	}

//完整的反魔法外罩
using System;
using System.Collections;

// Token: 0x0200002C RID: 44
public partial class AntiMagicShell : SpellCard
{
	// Token: 0x060000B3 RID: 179 RVA: 0x000085D0 File Offset: 0x000067D0
	public override IEnumerator Cast(Character target)
	{
		//target.As<Minion>().HasSpellshield = true;
		//yield break;
		foreach (Minion minion in this.Player.Minions)
		{
			minion.AddAttackModifier(new Func<int, int>(this.AntiMagicModifier));
			//minion.CurrentHealth++;
			minion.AddHealthModifier(new Func<int, int>(this.AntiMagicModifier));
      minion.HasSpellshield = true;
		}
		yield break;
	}
	public int AntiMagicModifier(int value)
	{
		return value + 2;
	}
}

public override IEnumerator Cast(Character target)
{
  foreach (Minion minion in this.Player.Minions)
  {
    Minion scopedMinion = minion;
    DisposableEvent<TurnEvent> disposable = null;
    minion.AddAttackModifier(new Func<int, int>(this.HornofWinterModifier));
    disposable = minion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, scopedMinion, disposable));
  }
  this.Player.Hero.AddAttackModifier(new Func<int, int>(this.HornofWinterModifier));
  this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add((TurnEvent x) => this.OnTurnEnd(x, this.Player.Hero, this.TurnEndSubscription));
  yield break;
}

using System;
using System.Collections;

public class GrandInquisitor : MinionCard
{
	public GrandInquisitor()
	{
		this.Name = "大审判官";
		this.Description = "Battlecry: Draw 2 cards. Discard any spell drawn this way.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.Draw(new Func<BaseCard, IEnumerator>(this.GrandInquisitorDraw));
		yield return this.Player.Draw(new Func<BaseCard, IEnumerator>(this.GrandInquisitorDraw));
		yield break;
	}

	public IEnumerator GrandInquisitorDraw(BaseCard card)
	{
		if (card is SpellCard)
		{
			yield return card.Discard();
		}
		yield break;
	}
}

//嗜血伏击者
using System;
using System.Collections;

public class RaiseDead : SpellCard
{
	public RaiseDead()
	{
		this.Name = "嗜血伏击者";
		this.Description = "Summon a 3/3 Ghoul. Costs (1) less for each minion that died this turn.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.AddCostModifier(new Func<int, int>(this.MinionDiedModifier));
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count < 7;
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.IsDamaged());
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.SummonMinion(new AmbushingGeist());
		yield break;
	}

	public int MinionDiedModifier(int cost)
	{
		return cost - GameManager.Instance.CurrentTurnDeadMinions;
	}
}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().AddAttackModifier(new Func<int, int>(this.AttackModifier));
		((Minion)target).Poison();
		yield break;
	}

	public int AttackModifier(int attack)
	{
		return attack + 2;
	}
	

HeroClass
????Neutral,中性
????Druid,德鲁伊
????Hunter,猎人
????Mage,法师
????Paladin,圣骑士
????Priest,牧师
????Rogue,潜行者
????Shaman,萨满
????Warlock,术士
????Warrior,战士
????DeathKnight,死亡骑士
????Monk,武僧
????DemonHunter,恶魔猎手

