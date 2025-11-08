using System;
using System.Collections;
using UnityEngine;

public class TempleConcubine : MinionCard
{
	public TempleConcubine()
	{
		this.Name = "灾祸之龙";
		this.Description = "When attacking, you get a +2/+0 effect until the end of the turn.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 5;
		this.Mechanics.OnMinionPreAttack.Add(new Func<MinionPreAttackEvent, IEnumerator>(this.OnMinionPreAttack));
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionPreAttack(MinionPreAttackEvent evt)
	{
		if (evt.Minion == this.Minion)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			base.AddAttackModifier(new Func<int, int>(this.TempleConcubineAttackModifier));
			this.TempleConcubineAttacked = 1;
		}
		yield break;
	}

	public int TempleConcubineAttackModifier(int attack)
	{
		return attack + 2;
	}

	public int TempleConcubineRemoveModifier(int attack)
	{
		return attack - 2;
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (this.TempleConcubineAttacked == 1)
		{
			base.AddAttackModifier(new Func<int, int>(this.TempleConcubineRemoveModifier));
			this.TempleConcubineAttacked = 0;
		}
		yield break;
	}

	public int TempleConcubineAttacked;
}
