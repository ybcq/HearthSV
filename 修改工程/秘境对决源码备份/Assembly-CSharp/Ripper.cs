using System;
using System.Collections;
using UnityEngine;

public class Ripper : WeaponCard
{
	public Ripper()
	{
		this.Name = "鲜血花园";
		this.Description = "Count 4. At the end of my turn, deal 1 damage to both Hero";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 1;
		this.BaseAttack = 0;
		this.BaseDurability = 4;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeWeapon();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			int currentDurability = this.Weapon.CurrentDurability;
			this.Weapon.CurrentDurability = currentDurability - 1;
			yield return this.Weapon.CurrentDurability;
			if (this.Weapon.CurrentDurability <= 0)
			{
				yield return this.Player.DestroyWeapon();
			}
		}
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 1);
			yield return this.Player.Enemy.Hero.Damage(null, 1);
			InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Hero.Controller, 1);
			yield return this.Player.Hero.Damage(null, 1);
			yield return this.Player.Enemy.Hero.CheckDeath();
			yield return this.Player.Hero.CheckDeath();
		}
		yield break;
	}
}
