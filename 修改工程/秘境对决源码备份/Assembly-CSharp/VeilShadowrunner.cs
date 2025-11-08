using System;

public class VeilShadowrunner : MinionCard
{
	public VeilShadowrunner()
	{
		this.Name = "霜背雪山狼";
		this.Description = "Charge.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.CurrentHealth = 3;
		this.HasCharge = true;
	}
}
