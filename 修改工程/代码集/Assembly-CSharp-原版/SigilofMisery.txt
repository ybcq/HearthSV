using System;
using System.Collections;

public class SigilofMisery : SpellCard
{
	public SigilofMisery()
	{
		this.Name = "Sigil of Misery";
		this.Description = "Give your opponent a Misery card with Held: Whenever you draw a card, your opponent draws a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Enemy.AddCardToHand(new Misery());
		yield break;
	}
}
