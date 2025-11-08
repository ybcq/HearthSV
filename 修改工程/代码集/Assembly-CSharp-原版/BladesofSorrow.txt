using System;
using System.Collections;
using UnityEngine;

public class BladesofSorrow : WeaponCard
{
	public BladesofSorrow()
	{
		this.Name = "Blades of Sorrow";
		this.Description = "Whenever a friendly minion dies, lose 1 Attack.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseDurability = 3;
		this.Mechanics.OnMinionDied.Add(new Func<MinionDiedEvent, IEnumerator>(this.OnMinionDied));
		base.InitializeWeapon();
	}

	public IEnumerator OnMinionDied(MinionDiedEvent evt)
	{
		if (evt.Minion.IsFriendlyOf(this.Player.Hero))
		{
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Weapon.AddAttackModifier(new Func<int, int>(this.BladesofSorrowModifier));
		}
		yield break;
	}

	public int BladesofSorrowModifier(int attack)
	{
		return attack - 1;
	}
}
