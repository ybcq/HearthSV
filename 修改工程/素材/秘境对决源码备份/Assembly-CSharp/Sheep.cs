using System;

public class Sheep : MinionCard
{
	public Sheep()
	{
		this.Name = "鬼灵战马";
		this.Description = "Evasion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 6;
		this.IsEvasive = true;
		base.InitializeMinion();
	}
}
