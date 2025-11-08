using System;

public class ChargeGhoul : MinionCard
{
	public ChargeGhoul()
	{
		this.Name = "Ghoul";
		this.Description = "Charge";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.Collectible = false;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.HasCharge = true;
		base.InitializeMinion();
	}
}
