using System;
using System.Collections;
using UnityEngine;

public class RuneofSpellshattering : SpellCard
{
	public RuneofSpellshattering()
	{
		this.Name = "破法符文";
		this.Description = "Give your weapon \"Whenever your hero attacks a minion, Silence it.\"";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.Collectible = false;
		this.BaseCost = 1;
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
		if (evt.Target is Minion)
		{
			weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			evt.Target.As<Minion>().Silence();
		}
		yield break;
	}
}
