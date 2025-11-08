using System;

public class BonyConstruct : MinionCard
{
	public BonyConstruct()
	{
		this.Name = "Bony Construct";
		this.Description = "Cleave";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 4;
		this.HasCleave = true;
		base.InitializeMinion();
	}
}
