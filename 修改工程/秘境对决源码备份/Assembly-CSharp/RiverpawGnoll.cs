using System;

public class RiverpawGnoll : MinionCard
{
	public RiverpawGnoll()
	{
		this.Name = "炽翼信徒";
		this.Description = "DivineShield.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.HasDivineShield = true;
		base.InitializeMinion();
	}
}
