using System;
using System.Collections;
using UnityEngine;

public class SpellturnGlaive : WeaponCard
{
	public SpellturnGlaive()
	{
		this.Name = "Spellturn Glaive";
		this.Description = "Whenever your opponent casts a spell, gain +1 Durability.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseDurability = 2;
		this.Mechanics.OnSpellCasted.Add(new Func<SpellCastedEvent, IEnumerator>(this.OnSpellCasted));
		base.InitializeWeapon();
	}

	public IEnumerator OnSpellCasted(SpellCastedEvent evt)
	{
		if (evt.Player == this.Player.Enemy)
		{
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Weapon.CurrentDurability++;
			this.Weapon.MaxDurability++;
			this.Weapon.Controller.UpdateNumbers();
		}
		yield break;
	}
}
