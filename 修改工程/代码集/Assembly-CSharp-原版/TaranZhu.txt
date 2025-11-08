using System;
using System.Collections;
using UnityEngine;

public class TaranZhu : MinionCard
{
	public TaranZhu()
	{
		this.Name = "Taran Zhu";
		this.Description = "Stealth. Battlecry: Give your hero Stealth until your next turn.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Legendary;
		this.BaseCost = 8;
		this.BaseAttack = 7;
		this.BaseHealth = 5;
		this.IsStealth = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		this.Player.Hero.IsStealth = true;
		this.TurnStartSubscription = EventManager.Instance.TurnStartHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent turnEvent)
	{
		if (turnEvent.Player == this.Player)
		{
			this.Player.Hero.IsStealth = false;
			this.TurnStartSubscription.Dispose();
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public IDisposable TurnStartSubscription;
}
