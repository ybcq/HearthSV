using System;

public class RockhideBoar : MinionCard
{
	public RockhideBoar()
	{
		this.Name = "Rockhide Boar";
		this.Description = "Charge. Taunt.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 3;
		this.HasCharge = true;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
