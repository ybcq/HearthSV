using System;

public class Sheep : MinionCard
{
	public Sheep()
	{
		this.Name = "Sheep";
		this.Description = string.Empty;
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Collectible = false;
		base.InitializeMinion();
	}
}
