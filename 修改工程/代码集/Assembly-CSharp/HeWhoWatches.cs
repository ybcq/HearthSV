using System;

public class HeWhoWatches : MinionCard
{
	public HeWhoWatches()
	{
		this.Name = "被封印的古代巨龙";
		this.Description = "Can't attack. Taunt.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 3;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.CantAttack = true;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
