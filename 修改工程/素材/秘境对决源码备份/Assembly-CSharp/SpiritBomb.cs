using System;
using System.Collections;

public class SpiritBomb : SpellCard
{
	public SpiritBomb()
	{
		this.Name = "致命诅咒";
		this.Description = "Deal 8 damage to an enemy minions.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.EnemyMinions;
		this.Collectible = false;
		this.BaseCost = 9;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player.Enemy).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 8 + this.Player.GetSpellPower());
		yield return targetMinion.Damage(null, 8 + this.Player.GetSpellPower());
		yield return targetMinion.CheckDeath();
		yield break;
	}
}
