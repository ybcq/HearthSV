using System;
using System.Collections;

public class EssenceofDesire : SpellCard
{
	public EssenceofDesire()
	{
		this.Name = "Essence of Desire";
		this.Description = "Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Draw(null);
		yield break;
	}
}
