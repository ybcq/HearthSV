using System;
using System.Collections;
using UnityEngine;

public class RuneofLichbane : SpellCard
{
	public RuneofLichbane()
	{
		this.Name = "巫妖符文";
		this.Description = "Give your weapon +3 Attack and \"Whenever your hero attacks, restore 2 Health to it.\"";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.HasWeapon();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Weapon.AddAttackModifier(new Func<int, int>(this.RuneofLichbaneModifier));
		this.Player.Weapon.Mechanics.OnHeroPreAttack.Add((HeroPreAttackEvent x) => this.OnHeroPreAttack(x, this.Player.Weapon));
		yield break;
	}

	public int RuneofLichbaneModifier(int attack)
	{
		return attack + 3;
	}

	public IEnumerator OnHeroPreAttack(HeroPreAttackEvent evt, Weapon weapon)
	{
		if (evt.Hero == weapon.Player.Hero)
		{
			weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			yield return weapon.Player.Hero.Heal(2);
		}
		yield break;
	}
}
