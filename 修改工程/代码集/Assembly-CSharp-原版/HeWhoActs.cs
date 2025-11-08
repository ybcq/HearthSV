using System;
using System.Collections;
using UnityEngine;

public class HeWhoActs : MinionCard
{
	public HeWhoActs()
	{
		this.Name = "He Who Acts";
		this.Description = "Charge. Evasion. Whenever you draw a card, reduce its cost by (2).";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.HasCharge = true;
		this.IsEvasive = true;
		this.Mechanics.OnCardDrawn.Add(new Func<CardDrawnEvent, IEnumerator>(this.OnCardDrawn));
		base.InitializeMinion();
	}

	public IEnumerator OnCardDrawn(CardDrawnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			evt.Card.AddCostModifier(new Func<int, int>(this.HeWhoActsCostModifier));
		}
		yield break;
	}

	public int HeWhoActsCostModifier(int cost)
	{
		return cost - 2;
	}
}
