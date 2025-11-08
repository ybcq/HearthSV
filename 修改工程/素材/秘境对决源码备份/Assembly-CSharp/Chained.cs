using System;
using System.Collections;

public class Chained : SpellCard
{
	public Chained()
	{
		this.Name = "侠盗的仁义";
		this.Description = "Deal the same damages to an enemy minions as your cards.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, this.Player.Hand.Count + this.Player.GetSpellPower());
		yield return targetMinion.Damage(null, this.Player.Hand.Count + this.Player.GetSpellPower());
		yield return targetMinion.CheckDeath();
		yield break;
	}
}
