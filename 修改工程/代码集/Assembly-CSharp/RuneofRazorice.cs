using System;
using System.Collections;
using UnityEngine;

public class RuneofRazorice : SpellCard
{
	public RuneofRazorice()
	{
		this.Name = "剃刀之符文";
		this.Description = "Give your weapon \"Whenever your hero attacks a character, Freeze it.\"";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.HasWeapon();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Weapon.Mechanics.OnAttacked.Add((AttackedEvent x) => this.OnAttacked(x, this.Player.Weapon));
		yield break;
	}

	public IEnumerator OnAttacked(AttackedEvent evt, Weapon weapon)
	{
		weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		evt.Target.Freeze();
		yield break;
	}
}
