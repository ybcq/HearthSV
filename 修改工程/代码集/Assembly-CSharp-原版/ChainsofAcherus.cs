using System;
using System.Collections;

public class ChainsofAcherus : SpellCard
{
	public ChainsofAcherus()
	{
		this.Name = "Chains of Acherus";
		this.Description = "Discover a terrifying Presence.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.DiscoverCard(new FrostPresence(), new BloodPresence(), new UnholyPresence());
		yield break;
	}
}
