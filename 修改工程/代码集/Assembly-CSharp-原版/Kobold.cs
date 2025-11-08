using System;

public class Kobold : MinionCard
{
	public Kobold()
	{
		this.Name = "Kobold";
		this.Description = string.Empty;
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.Collectible = false;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		base.InitializeMinion();
	}
}
