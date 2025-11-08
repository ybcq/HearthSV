using System;
using System.Collections;

public class SeethingRage : SpellCard
{
	public SeethingRage()
	{
		this.Name = "森林的反扑";
		this.Description = "Deal 2 damage to an enemy minions and add a card to your hand.";
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
		Minion targetMinion = (Minion)target;
		InterfaceManager.Instance.SpawnDamageSplatOn(targetMinion.Controller, 2 + this.Player.GetSpellPower());
		yield return targetMinion.Damage(null, 2 + this.Player.GetSpellPower());
		yield return targetMinion.CheckDeath();
		yield return this.Player.AddCardToHand(new HighWarlordNajentus());
		yield break;
	}
}
