using System;
using System.Collections;

public class SigilofChains : SpellCard
{
	public SigilofChains()
	{
		this.Name = "Sigil of Chains";
		this.Description = "Give your opponent a Chained card with Held: Your Minions cost (2) more.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Enemy.AddCardToHand(new Chained());
		yield break;
	}
}
