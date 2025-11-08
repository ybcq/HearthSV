using System;
using System.Collections;
using UnityEngine;

public class LeotherasUnleashed : MinionCard
{
	public LeotherasUnleashed()
	{
		this.Name = "Leotheras Unleashed";
		this.Description = "Cleave. At the end of your turn, transform this minion into Leotheras the Blind.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Demon;
		this.Collectible = false;
		this.BaseCost = 7;
		this.BaseAttack = 5;
		this.BaseHealth = 7;
		this.HasCleave = true;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Minion.TransformInto(new LeotherastheBlind());
		}
		yield break;
	}
}
