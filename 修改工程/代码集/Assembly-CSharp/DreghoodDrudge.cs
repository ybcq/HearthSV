using System;

public class DreghoodDrudge : MinionCard
{
	public DreghoodDrudge()
	{
		this.Name = "神谕的贵族";
		this.Description = "Can't attack Heroes.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 6;
		this.BaseHealth = 6;
		base.InitializeMinion();
	}
}
