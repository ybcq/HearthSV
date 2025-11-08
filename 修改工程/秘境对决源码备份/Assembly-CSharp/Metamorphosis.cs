using System;
using System.Collections;
using System.Linq;

public class Metamorphosis : SpellCard
{
	public Metamorphosis()
	{
		this.Name = "灵魂狩猎";
		this.Description = "Deal 3 damage to an enemy minions and add a card into grave.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 3 + this.Player.GetSpellPower());
		yield return targetMinion.Damage(null, 3 + this.Player.GetSpellPower());
		yield return targetMinion.CheckDeath();
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Any<Minion>();
	}
}
