using System;
using System.Collections;
using System.Linq;

public class SpectralSight : SpellCard
{
	public SpectralSight()
	{
		this.Name = "崭新的命运";
		this.Description = "DisCard all cards & Draw the same count cards.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int count = this.Player.Hand.Count;
		while (this.Player.Hand.Count > 0)
		{
			BaseCard baseCard = RNG.RandomItemFrom<BaseCard>((from m in this.Player.Hand
			select m).ToList<BaseCard>());
			if (baseCard != null)
			{
				yield return baseCard.Discard();
			}
		}
		yield return this.Player.Draw(count, null);
		yield break;
	}
}
