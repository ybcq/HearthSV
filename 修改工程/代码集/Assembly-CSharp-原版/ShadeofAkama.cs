using System;

public class ShadeofAkama : MinionCard
{
	public ShadeofAkama()
	{
		this.Name = "Shade of Akama";
		this.Description = "Stealth. Destroy any minion damaged by this.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.Collectible = false;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.IsStealth = true;
		this.HasPoison = true;
		base.InitializeMinion();
	}
}
