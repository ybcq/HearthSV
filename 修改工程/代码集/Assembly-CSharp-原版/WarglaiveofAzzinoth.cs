using System;
using System.Collections;

public class WarglaiveofAzzinoth : WeaponCard
{
	public WarglaiveofAzzinoth()
	{
		this.Name = "Warglaive of Azzinoth";
		this.Description = "If you already have a Warglaive of Azzinoth equipped, give it Windfury and +3 Durability.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseDurability = 3;
		this.Mechanics.OnWeaponPreEquip.Add(new Func<WeaponPreEquipEvent, IEnumerator>(this.OnWeaponPreEquip));
		base.InitializeWeapon();
	}

	public IEnumerator OnWeaponPreEquip(WeaponPreEquipEvent evt)
	{
		if (evt.Player == this.Player && evt.Weapon == this && this.Player.HasWeapon() && this.Player.Weapon.Card is WarglaiveofAzzinoth)
		{
			evt.Cancel();
			this.Player.Weapon.HasWindfury = true;
			this.Player.Weapon.CurrentDurability += 3;
			this.Player.Weapon.MaxDurability += 3;
		}
		yield break;
	}
}
