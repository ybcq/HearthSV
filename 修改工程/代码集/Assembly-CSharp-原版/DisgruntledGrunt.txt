using System;

public class DisgruntledGrunt : MinionCard
{
	public DisgruntledGrunt()
	{
		this.Name = "Disgruntled Grunt";
		this.Description = string.Empty;
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 5;
		base.InitializeMinion();
	}
}
