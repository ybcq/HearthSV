using System;
using System.Collections;
using UnityEngine;

public class DreghoodDrudge : MinionCard
{
	public DreghoodDrudge()
	{
		this.Name = "Dreghood Drudge";
		this.Description = "Can't attack Heroes. Enrage: Can attack Heroes.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.Mechanics.OnEnraged.Add(new Func<Minion, IEnumerator>(this.OnEnraged));
		this.Mechanics.OnDisenraged.Add(new Func<Minion, IEnumerator>(this.OnDisenraged));
		base.InitializeMinion();
	}

	public IEnumerator OnEnraged(Minion minion)
	{
		this.CantAttackHeroes = false;
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnDisenraged(Minion minion)
	{
		this.CantAttackHeroes = true;
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public int ApplyEnrageModifier(int attack)
	{
		return attack + 2;
	}
}
