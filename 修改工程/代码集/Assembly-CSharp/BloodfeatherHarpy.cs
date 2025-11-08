using System;

public class BloodfeatherHarpy : MinionCard
{
	public BloodfeatherHarpy()
	{
		this.Name = "灰烬旋涡";
		this.Description = "Windfury";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 4;
		this.BaseHealth = 5;
		this.HasWindfury = true;
		base.InitializeMinion();
	}
}
