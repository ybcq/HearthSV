using System;
using System.Collections;
using UnityEngine;

public class LeiShen : MinionCard
{
	public LeiShen()
	{
		this.Name = "拉佐格尔";
		this.Description = "At the start of your turn, give your minions +3 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 6;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public int ApplyLeiShenModifier(int attack)
	{
		return attack + 3;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			foreach (Minion minion in this.Player.Minions)
			{
				if (minion.Card.MinionType != MinionType.Totem)
				{
					minion.AddAttackModifier(new Func<int, int>(this.ApplyLeiShenModifier));
				}
			}
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
