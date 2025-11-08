using System;
using System.Collections;
using UnityEngine;

public class TimeKeeper : MinionCard
{
	public TimeKeeper()
	{
		this.Name = "Time Keeper";
		this.Description = "Battlecry: Whenever another friendly minion dies this turn, return it to your hand.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 7;
		this.BaseAttack = 7;
		this.BaseHealth = 4;
		this.MinionDiedSubscription = this.Mechanics.OnMinionDied.Add(new Func<MinionDiedEvent, IEnumerator>(this.OnMinionDied));
		this.TurnEndSubscription = this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnMinionDied(MinionDiedEvent evt)
	{
		Minion targetMinion = evt.Minion;
		if (targetMinion.IsFriendlyOf(this.Minion))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			yield return this.Player.AddCardToHand(targetMinion.Card.Copy());
		}
		yield break;
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		this.MinionDiedSubscription.Dispose();
		this.TurnEndSubscription.Dispose();
		yield break;
	}

	private DisposableEvent<MinionDiedEvent> MinionDiedSubscription;

	private DisposableEvent<TurnEvent> TurnEndSubscription;
}
