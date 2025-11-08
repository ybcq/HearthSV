using System;

public class SeaElemental : MinionCard
{
	public SeaElemental()
	{
		this.Name = "邪爆巨像";
		this.Description = "Taunt.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 7;
		this.BaseAttack = 7;
		this.BaseHealth = 7;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
