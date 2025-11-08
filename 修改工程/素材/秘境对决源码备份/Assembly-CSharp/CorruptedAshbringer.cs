using System;

public class CorruptedAshbringer : WeaponCard
{
	public CorruptedAshbringer()
	{
		this.Name = "灰烬使者";
		this.Description = string.Empty;
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseDurability = 2;
		base.InitializeWeapon();
	}
}
