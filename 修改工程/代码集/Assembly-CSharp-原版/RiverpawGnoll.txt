using System;
using System.Collections;
using UnityEngine;

public class RiverpawGnoll : MinionCard
{
	public RiverpawGnoll()
	{
		this.Name = "Riverpaw Gnoll";
		this.Description = "Enrage: +2 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.EnrageModifier = new Func<int, int>(this.ApplyEnrageModifier);
		this.Mechanics.OnEnraged.Add(new Func<Minion, IEnumerator>(this.OnEnraged));
		this.Mechanics.OnDisenraged.Add(new Func<Minion, IEnumerator>(this.OnDisenraged));
		base.InitializeMinion();
	}

	public IEnumerator OnEnraged(Minion minion)
	{
		this.Minion.AddAttackModifier(this.EnrageModifier);
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnDisenraged(Minion minion)
	{
		this.Minion.RemoveAttackModifier(this.EnrageModifier);
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public int ApplyEnrageModifier(int attack)
	{
		return attack + 2;
	}

	public Func<int, int> EnrageModifier;
}
