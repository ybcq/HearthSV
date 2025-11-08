using System;
using System.Collections;
using System.Linq;

public class FuryoftheIllidari : SpellCard
{
	public FuryoftheIllidari()
	{
		this.Name = "Fury of the Illidari";
		this.Description = "Give your minions +4 Attack (wherever they are).";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 9;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Minion minion in this.Player.Minions)
		{
			minion.AddAttackModifier(new Func<int, int>(this.FuryModifier));
		}
		foreach (MinionCard minionCard in this.Player.Hand.OfType<MinionCard>())
		{
			minionCard.AddAttackModifier(new Func<int, int>(this.FuryModifier));
		}
		foreach (MinionCard minionCard2 in this.Player.Deck.OfType<MinionCard>())
		{
			minionCard2.AddAttackModifier(new Func<int, int>(this.FuryModifier));
		}
		yield break;
	}

	public int FuryModifier(int attack)
	{
		return attack + 4;
	}
}
