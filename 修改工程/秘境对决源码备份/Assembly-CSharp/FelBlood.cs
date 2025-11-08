using System;
using System.Collections;

public class FelBlood : SpellCard
{
	public FelBlood()
	{
		this.Name = "血红净化";
		this.Description = "Deal 2 damage to your hero and destroy an enemy minion.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Hero.Controller, 2 + this.Player.GetSpellPower());
		yield return this.Player.Hero.Damage(null, 2 + this.Player.GetSpellPower());
		yield return this.Player.Hero.CheckDeath();
		yield return target.As<Minion>().Destroy();
		yield return target.CheckDeath();
		yield break;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player.Enemy).Count > 0;
	}
}
