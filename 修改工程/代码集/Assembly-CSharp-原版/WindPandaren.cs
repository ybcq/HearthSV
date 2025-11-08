using System;

public class WindPandaren : MinionCard
{
	public WindPandaren()
	{
		this.Name = "Wind Pandaren";
		this.Description = "Windfury";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.Collectible = false;
		this.HasWindfury = true;
		base.InitializeMinion();
	}
}
