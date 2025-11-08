using System;

public class FireElemental : MinionCard
{
	public FireElemental()
	{
		this.Name = "青铜守卫(复)";
		this.Description = "Divine Shield, Relives";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 1;
		this.HasDivineShield = true;
		base.InitializeMinion();
	}
}
