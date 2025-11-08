using System;
using System.Collections;

public class Imprison : SpellCard
{
	public Imprison()
	{
		this.Name = "疾风怒涛";
		this.Description = "Deal the same damages to an enemy minion as your cards.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, this.Player.Minions.Count + this.Player.GetSpellPower());
		yield return targetMinion.Damage(null, this.Player.Minions.Count + this.Player.GetSpellPower());
		yield return targetMinion.CheckDeath();
		yield break;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player.Enemy).Count > 0;
	}
}
