using System;

public class ElwynnForestBear : MinionCard
{
	public ElwynnForestBear()
	{
		this.Name = "Elwynn Forest Bear";
		this.Description = "Taunt";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
