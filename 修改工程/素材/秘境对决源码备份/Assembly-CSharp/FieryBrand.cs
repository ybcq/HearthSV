using System;
using System.Collections;

public class FieryBrand : SpellCard
{
	public FieryBrand()
	{
		this.Name = "炽热吐息";
		this.Description = "Deal 2 damage to an enemy minions.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player.Enemy).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 2 + this.Player.GetSpellPower());
		yield return targetMinion.Damage(null, 2 + this.Player.GetSpellPower());
		yield return targetMinion.CheckDeath();
		yield break;
	}
}
