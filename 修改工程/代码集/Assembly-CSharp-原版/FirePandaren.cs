using System;

public class FirePandaren : MinionCard
{
	public FirePandaren()
	{
		this.Name = "Fire Pandaren";
		this.Description = "Charge";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.Collectible = false;
		this.HasCharge = true;
		base.InitializeMinion();
	}
}
