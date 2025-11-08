using System;
using System.Collections;

public class Misery : SpellCard
{
	public Misery()
	{
		this.Name = "鲜血的吻唇";
		this.Description = "Inflicts 2 damage to an enemy's entourage. Recover 2 hit points of your main warrior.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player.Enemy).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 2 + this.Player.GetSpellPower());
		yield return target.As<Minion>().Damage(null, 2 + this.Player.GetSpellPower());
		yield return target.As<Minion>().CheckDeath();
		yield return this.Player.Hero.Heal(2);
		yield break;
	}
}
