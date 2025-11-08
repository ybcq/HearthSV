using System;

public class NormalGhoul : MinionCard
{
	public NormalGhoul()
	{
		this.Name = "Ghoul";
		this.Description = string.Empty;
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.Collectible = false;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		base.InitializeMinion();
	}
}
