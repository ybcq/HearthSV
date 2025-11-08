using System;
using System.Collections;

public class HornofWinter : SpellCard
{
	public HornofWinter()
	{
		this.Name = "湮灭";
		this.Description = "Destroy a enenmy minion. Deal your Hero the same attack with the Health of Target";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, target.As<Minion>().CurrentHealth + this.Player.GetSpellPower());
		yield return this.Player.Hero.Damage(this.Player.Hero, target.As<Minion>().CurrentHealth + this.Player.GetSpellPower());
		yield return target.As<Minion>().Destroy();
		yield return target.CheckDeath();
		yield break;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player.Enemy).Count > 0;
	}
}
