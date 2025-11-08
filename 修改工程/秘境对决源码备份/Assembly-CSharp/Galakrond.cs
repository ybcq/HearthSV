using System;
using System.Collections;
using UnityEngine;

public class Galakrond : MinionCard
{
	public Galakrond()
	{
		this.Name = "狂野的拉佐格尔";
		this.Description = "At the end of your turn, gain +1/+1 for each Dragon you have.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 8;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private int GalakrondModifier(int value)
	{
		return value + this.DestroyedDragons;
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			this.DestroyedDragons = 0;
			foreach (Minion minion in this.Player.Minions)
			{
				if (minion != this.Minion && minion.Card.MinionType == MinionType.Dragon)
				{
					this.DestroyedDragons++;
				}
			}
			if (this.DestroyedDragons > 0)
			{
				yield return new WaitForSeconds(0.25f);
				this.Minion.AddAttackModifier(new Func<int, int>(this.GalakrondModifier));
				this.Minion.AddHealthModifier(new Func<int, int>(this.GalakrondModifier));
				this.Minion.CurrentHealth += this.DestroyedDragons;
			}
			yield break;
		}
		yield break;
	}

	private int DestroyedDragons;
}
