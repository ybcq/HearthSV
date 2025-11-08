using System;
using System.Collections;
using System.Linq;

public class SpectralSight : SpellCard
{
	public SpectralSight()
	{
		this.Name = "Spectral Sight";
		this.Description = "Reveal your opponent's hand. If they have a Demon or a Stealth card, draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (BaseCard baseCard in this.Player.Enemy.Hand)
		{
			baseCard.Reveal();
		}
		if (this.Player.Enemy.Hand.OfType<MinionCard>().Any((MinionCard c) => c.MinionType == MinionType.Demon || c.IsStealth))
		{
			yield return this.Player.Draw(null);
		}
		yield break;
	}
}
