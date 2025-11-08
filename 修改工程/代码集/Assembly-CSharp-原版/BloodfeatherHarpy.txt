using System;

public class BloodfeatherHarpy : MinionCard
{
	public BloodfeatherHarpy()
	{
		this.Name = "Bloodfeather Harpy";
		this.Description = "Windfury";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.HasWindfury = true;
		base.InitializeMinion();
	}
}
