using System;

public class EarthPandaren : MinionCard
{
	public EarthPandaren()
	{
		this.Name = "Earth Pandaren";
		this.Description = "Taunt";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.Collectible = false;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
