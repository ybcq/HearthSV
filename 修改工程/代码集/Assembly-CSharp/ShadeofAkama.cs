using System;

public class ShadeofAkama : MinionCard
{
	public ShadeofAkama()
	{
		this.Name = "赤羽的阴影";
		this.Description = "Gharge. Destroy any minion damaged by this.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.HasCharge = true;
		this.HasPoison = true;
		base.InitializeMinion();
	}
}
