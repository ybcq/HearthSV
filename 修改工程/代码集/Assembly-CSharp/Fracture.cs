using System;
using System.Collections;

public class Fracture : SpellCard
{
	public Fracture()
	{
		this.Name = "龙之怒";
		this.Description = "Deal 6 damage to an enemy minion. ";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Damage(null, 6 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield break;
	}
}
