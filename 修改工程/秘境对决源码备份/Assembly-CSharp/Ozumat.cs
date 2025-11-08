using System;

public class Ozumat : MinionCard
{
	public Ozumat()
	{
		this.Name = "穆克拉的大表哥";
		this.Description = "So strong! And only 6 Mana?!";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 10;
		this.BaseHealth = 10;
		this.Collectible = false;
		base.InitializeMinion();
	}
}
