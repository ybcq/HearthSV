using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoulCleave : SpellCard
{
	public SoulCleave()
	{
		this.Name = "Soul Cleave";
		this.Description = "Deal 2 damage to all enemy minions. Summon a Soul Fragment for each.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<Minion> enemyMinions = this.Player.Enemy.Minions.ToList<Minion>();
		int damagedMinions = 0;
		foreach (Minion minion in enemyMinions)
		{
			damagedMinions++;
			yield return minion.Damage(null, 2 + this.Player.GetSpellPower());
		}
		foreach (Minion minion2 in enemyMinions)
		{
			yield return minion2.CheckDeath();
		}
		for (int i = 0; i < damagedMinions; i++)
		{
			yield return this.Player.SummonMinion(new SoulFragment());
		}
		yield break;
	}
}
