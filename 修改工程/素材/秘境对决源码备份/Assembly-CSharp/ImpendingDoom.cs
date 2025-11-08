using System;
using System.Collections;
using System.Linq;

public class ImpendingDoom : SpellCard
{
	public ImpendingDoom()
	{
		this.Name = "漆黑法典";
		this.Description = "Remove an enemy minion which's health is less than 3.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().Mechanics.RemoveAll();
		yield return target.As<Minion>().Destroy();
		yield return target.CheckDeath();
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.CurrentHealth <= 3);
	}

	public override bool CanTarget(Character target)
	{
		return target != null && target.IsMinion() && target.IsFriendlyOf(this.Player.Enemy.Hero) && !target.IsStealth && !target.HasSpellshield && target.CurrentHealth <= 3;
	}
}
