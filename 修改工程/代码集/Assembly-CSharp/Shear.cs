using System;
using System.Collections;

public class Shear : SpellCard
{
	public Shear()
	{
		this.Name = "破坏神的气息";
		this.Description = "Deal 4 damage to an enemy minion, restore 2 Health to your hero. If your hero's health is less than 10, this costs 4 less.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 5;
		base.AddCostModifier(new Func<int, int>(this.HealthModifier));
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Damage(null, 4 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield return this.Player.Hero.Heal(2);
		yield return this.Player.SummonMinion(new SoulFragment());
		yield break;
	}

	public int HealthModifier(int cost)
	{
		if (this.Player.Hero.CurrentHealth <= 10)
		{
			return cost - 4;
		}
		return cost;
	}
}
