using System;

public class FireElemental : MinionCard
{
	public FireElemental()
	{
		this.Name = "Fire Elemental";
		this.Description = string.Empty;
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.Collectible = false;
		this.BaseCost = 1;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		base.InitializeMinion();
	}
}
