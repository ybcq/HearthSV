using System;
using System.Collections;
using UnityEngine;

public class Murozond : MinionCard
{
	public Murozond()
	{
		this.Name = "微型战斗机甲";
		this.Description = "At the start of each turn, gain +1/+1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Mech;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.Golden = true;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public int MurozondModifier(int value)
	{
		return value + 2;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		this.Minion.AddAttackModifier(new Func<int, int>(this.MurozondModifier));
		this.Minion.Controller.UpdateNumbers();
		yield break;
	}
}
