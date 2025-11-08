using System;

public class FallenChampion : MinionCard
{
	public FallenChampion()
	{
		this.Name = "Fallen Champion";
		this.Description = "Taunt";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.Collectible = false;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
