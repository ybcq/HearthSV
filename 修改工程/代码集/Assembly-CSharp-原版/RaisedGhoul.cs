using System;

public class RaisedGhoul : MinionCard
{
	public RaisedGhoul()
	{
		this.Name = "Ghoul";
		this.Description = string.Empty;
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.Collectible = false;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		base.InitializeMinion();
	}
}
