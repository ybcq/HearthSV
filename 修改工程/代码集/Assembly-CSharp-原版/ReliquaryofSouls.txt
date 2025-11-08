using System;
using System.Collections;
using UnityEngine;

public class ReliquaryofSouls : MinionCard
{
	public ReliquaryofSouls()
	{
		this.Name = "Reliquary of Souls";
		this.Description = "At the end of your turn, add an Essence card to your hand.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 6;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			int random = RNG.RandomInteger(0, 2);
			if (random != 0)
			{
				if (random != 1)
				{
					if (random == 2)
					{
						yield return this.Player.AddCardToHand(new EssenceofAnger());
					}
				}
				else
				{
					yield return this.Player.AddCardToHand(new EssenceofSuffering());
				}
			}
			else
			{
				yield return this.Player.AddCardToHand(new EssenceofDesire());
			}
		}
		yield break;
	}
}
