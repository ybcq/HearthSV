using System;

public class Swarmling : MinionCard
{
	public Swarmling()
	{
		this.Name = "Swarmling";
		this.Description = "Charge";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.Collectible = false;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.HasCharge = true;
		base.InitializeMinion();
	}
}
