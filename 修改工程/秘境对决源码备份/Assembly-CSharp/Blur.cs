using System;
using System.Collections;

public class Blur : SpellCard
{
	public Blur()
	{
		this.Name = "智慧之光";
		this.Description = "Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
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
