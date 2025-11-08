using System;
using System.Collections;
using UnityEngine;

public class Frostbite : WeaponCard
{
	public Frostbite()
	{
		this.Name = "冰霜之刃";
		this.Description = "Your hero deals double damage to Frozen characters.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseDurability = 2;
		this.Mechanics.OnCharacterPreDamage.Add(new Func<CharacterPreDamageEvent, IEnumerator>(this.OnCharacterPreDamage));
		base.InitializeWeapon();
	}

	public IEnumerator OnCharacterPreDamage(CharacterPreDamageEvent evt)
	{
		if (evt.Character.IsEnemyOf(this.Player.Hero) && evt.Character.IsFrozen && evt.Attacker == this.Player.Hero)
		{
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			evt.DamageAmount *= 2;
		}
		yield break;
	}
}
