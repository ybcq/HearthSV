using System;
using System.Collections;

public class SigilofFlame : SpellCard
{
	public SigilofFlame()
	{
		this.Name = "Sigil of Flame";
		this.Description = "Give your opponent a Burning card with Held: At the start of your turn, deal 1 damage to your characters.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Enemy.AddCardToHand(new Burning());
		yield break;
	}
}
