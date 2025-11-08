using System;

public class SpitefulWraith : MinionCard
{
	public SpitefulWraith()
	{
		this.Name = "Spiteful Wraith";
		this.Description = string.Empty;
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.Collectible = false;
		this.BaseCost = 1;
		this.BaseAttack = 2;
		this.BaseHealth = 1;
		base.InitializeMinion();
	}
}
