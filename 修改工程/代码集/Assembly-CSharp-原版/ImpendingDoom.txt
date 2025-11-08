using System;
using System.Collections;

public class ImpendingDoom : SpellCard
{
	public ImpendingDoom()
	{
		this.Name = "Impending Doom";
		this.Description = "Put a copy of the last card you drew into your hand. It costs (1) more.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.Player.LastDrawnCard != null)
		{
			BaseCard card = this.Player.LastDrawnCard.Copy();
			card.AddCostModifier(new Func<int, int>(this.ImpendingDoomModifier));
			yield return this.Player.AddCardToHand(card);
		}
		yield break;
	}

	public int ImpendingDoomModifier(int cost)
	{
		return cost + 1;
	}
}
