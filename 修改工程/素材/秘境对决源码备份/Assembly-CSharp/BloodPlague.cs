using System;
using System.Collections;
using UnityEngine;

public class BloodPlague : SpellCard
{
	public BloodPlague()
	{
		this.Name = "血腥瘟疫";
		this.Description = "Choose a minion. Whenever it attacks, deal 3 damage to your opponent's hero.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.Mechanics.OnAttacked.Add((AttackedEvent x) => this.OnAttacked(x, targetMinion));
		yield break;
	}

	public IEnumerator OnAttacked(AttackedEvent evt, Minion self)
	{
		self.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 3);
		yield return this.Player.Enemy.Hero.Damage(null, 3);
		yield return this.Player.Enemy.Hero.CheckDeath();
		yield break;
	}
}
