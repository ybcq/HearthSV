using System;
using System.Collections;
using UnityEngine;

public class YulonsChampion : MinionCard
{
	public YulonsChampion()
	{
		this.Name = "尤隆冠军";
		this.Description = "Meditate: Gain Immune until the start of your next turn.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 4;
		this.BaseAttack = 6;
		this.BaseHealth = 2;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent turnEvent)
	{
		if (turnEvent.Player == this.Player && this.Minion.IsAlive())
		{
			this.Minion.IsImmune = false;
			this.Minion.Controller.UpdateSprites();
			this.TurnStartSubscription.Dispose();
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public IEnumerator Meditate(Player player)
	{
		if (this.Minion.IsAlive())
		{
			this.Minion.IsImmune = true;
			this.TurnStartSubscription = EventManager.Instance.TurnStartHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public IDisposable TurnStartSubscription;
}
