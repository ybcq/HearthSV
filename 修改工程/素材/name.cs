
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
