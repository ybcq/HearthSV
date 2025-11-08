using System;
using System.Collections;
using UnityEngine;

public class PartyCrashers : MinionCard
{
	public PartyCrashers()
	{
		this.Name = "Party Crashers";
		this.Description = "Whenever your opponent draws a card, gain +1/+1.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.Mechanics.OnCardDrawn.Add(new Func<CardDrawnEvent, IEnumerator>(this.OnCardDrawn));
		base.InitializeMinion();
	}

	public IEnumerator OnCardDrawn(CardDrawnEvent evt)
	{
		if (evt.Player == this.Player.Enemy)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Minion.AddAttackModifier(new Func<int, int>(this.PartyCrashersModifier));
			this.Minion.CurrentHealth++;
			this.Minion.AddHealthModifier(new Func<int, int>(this.PartyCrashersModifier));
		}
		yield break;
	}

	public int PartyCrashersModifier(int value)
	{
		return value + 1;
	}
}
