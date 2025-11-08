using System;

public class Ripper : WeaponCard
{
	public Ripper()
	{
		this.Name = "Ripper";
		this.Description = string.Empty;
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 1;
		this.BaseAttack = 3;
		this.BaseDurability = 1;
		base.InitializeWeapon();
	}
}
