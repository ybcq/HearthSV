using System;
using System.Collections;
using UnityEngine;

public class YulonsChampion : MinionCard
{
	public YulonsChampion()
	{
		this.Name = "Yu'lon's Champion";
		this.Description = "Battlecry: Gain Immune until the start of your next turn.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 6;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		this.Minion.IsImmune = true;
		this.TurnStartSubscription = EventManager.Instance.TurnStartHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent turnEvent)
	{
		if (turnEvent.Player == this.Player)
		{
			this.Minion.IsImmune = false;
			this.Minion.Controller.UpdateSprites();
			this.TurnStartSubscription.Dispose();
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public IDisposable TurnStartSubscription;
}
