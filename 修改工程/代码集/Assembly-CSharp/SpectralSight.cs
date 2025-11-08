using System;
using System.Collections;

public class SpectralSight : SpellCard
{
	public SpectralSight()
	{
		this.Name = "崭新的命运";
		this.Description = "DisCard all cards & Draw the same count cards.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int count = this.Player.Hand.Count;
		foreach (BaseCard baseCard in this.Player.Hand)
		{
			baseCard.Discard();
		}
		yield return this.Player.Draw(count, null);
		yield break;
	}
}
