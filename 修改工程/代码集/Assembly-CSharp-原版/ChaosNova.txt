using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChaosNova : SpellCard
{
	public ChaosNova()
	{
		this.Name = "Chaos Nova";
		this.Description = "Deal 3 damage to all enemy minions. Minions with an odd Attack take 5 damage instead.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 6;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<Minion> targetMinions = this.Player.Enemy.Minions.ToList<Minion>();
		foreach (Minion minion in targetMinions)
		{
			if (minion.CurrentAttack % 2 == 0)
			{
				yield return minion.Damage(null, 3 + this.Player.GetSpellPower());
			}
			else
			{
				yield return minion.Damage(null, 5 + this.Player.GetSpellPower());
			}
		}
		foreach (Minion minion2 in targetMinions)
		{
			yield return minion2.CheckDeath();
		}
		yield break;
	}
}
