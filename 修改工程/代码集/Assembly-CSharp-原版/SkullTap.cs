using System;
using System.Collections;

public class SkullTap : SpellCard
{
	public SkullTap()
	{
		this.Name = "Skull Tap";
		this.Description = "Return this to your hand. It costs (2) more. Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.AddCostModifier(new Func<int, int>(this.SkullTapModifier));
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.CostModifier += 2;
		yield return this.Player.AddCardToHand(this);
		yield return this.Player.Draw(null);
		yield break;
	}

	public int SkullTapModifier(int cost)
	{
		return cost + this.CostModifier;
	}

	public int CostModifier;
}
