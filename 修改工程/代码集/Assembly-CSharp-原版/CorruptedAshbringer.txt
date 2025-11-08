using System;

public class CorruptedAshbringer : WeaponCard
{
	public CorruptedAshbringer()
	{
		this.Name = "Corrupted Ashbringer";
		this.Description = string.Empty;
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseDurability = 3;
		this.Collectible = false;
		base.InitializeWeapon();
	}
}
