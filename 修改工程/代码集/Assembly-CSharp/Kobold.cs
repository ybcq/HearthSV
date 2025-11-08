using System;

public class Kobold : MinionCard
{
	public Kobold()
	{
		this.Name = "冬拥重装骑士";
		this.Description = "Freeze any character damaged by this minion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Murloc;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseHealth = 6;
		this.HasFreeze = true;
		base.InitializeMinion();
	}
}
