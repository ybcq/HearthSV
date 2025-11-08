using System;

public class BloodfeatherHarpy : MinionCard
{
	public BloodfeatherHarpy()
	{
		this.Name = "地下街快手";
		this.Description = "Charge.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.HasCharge = true;
		base.InitializeMinion();
	}
}
