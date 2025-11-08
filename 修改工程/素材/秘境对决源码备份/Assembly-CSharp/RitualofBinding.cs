using System;
using System.Collections;

public class RitualofBinding : SpellCard
{
	public RitualofBinding()
	{
		this.Name = "初级诅咒";
		this.Description = "Deal 2 damage to an enemy minions. Add a normal card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 3;
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
		yield return this.Player.AddCardToHand(new SoulCarve());
		yield break;
	}
}
