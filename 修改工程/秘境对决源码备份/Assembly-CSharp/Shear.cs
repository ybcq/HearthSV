using System;
using System.Collections;

public class Shear : SpellCard
{
	public Shear()
	{
		this.Name = "破坏神的气息";
		this.Description = "Deal 4 damage to an enemy minion, restore 2 Health to your hero. If your hero's health is less than 10, this costs 4 less.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 5;
		base.AddCostModifier(new Func<int, int>(this.CostModifier));
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 4 + this.Player.GetSpellPower());
		yield return target.Damage(null, 4 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield return this.Player.Hero.Heal(2);
		yield break;
	}

	public int CostModifier(int cost)
	{
		if (this.Player.Hero.CurrentHealth <= 10)
		{
			return cost - 4;
		}
		return cost;
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}
}
