using System;

public class FrozenGhoul : MinionCard
{
	public FrozenGhoul()
	{
		this.Name = "Ghoul";
		this.Description = string.Empty;
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.Collectible = false;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		base.InitializeMinion();
	}
}
