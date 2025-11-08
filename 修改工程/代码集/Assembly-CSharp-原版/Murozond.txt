using System;
using System.Collections;
using UnityEngine;

public class Murozond : MinionCard
{
	public Murozond()
	{
		this.Name = "Murozond";
		this.Description = "At the end of each turn, gain +1/+1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		this.Minion.AddAttackModifier(new Func<int, int>(this.MurozondModifier));
		this.Minion.CurrentHealth++;
		this.Minion.AddHealthModifier(new Func<int, int>(this.MurozondModifier));
		this.Minion.Controller.UpdateNumbers();
		yield break;
	}

	public int MurozondModifier(int value)
	{
		return value + 1;
	}
}
