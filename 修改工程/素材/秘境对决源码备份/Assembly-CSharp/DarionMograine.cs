using System;
using System.Collections;
using UnityEngine;

public class DarionMograine : MinionCard
{
	public DarionMograine()
	{
		this.Name = "达里安·莫格莱恩";
		this.Description = "Spellshield. Battlecry: Equip a 5/3 Corrupted Ashbringer.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 8;
		this.BaseAttack = 6;
		this.BaseHealth = 6;
		this.HasSpellshield = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character character)
	{
		yield return this.Player.EquipWeapon(new CorruptedAshbringer(), null);
		this.Player.Weapon.BaseDurability = 3;
		this.Player.Weapon.CurrentDurability = 3;
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
