using System;
using System.Collections;
using UnityEngine;

public class TigerStance : SpellCard
{
	public TigerStance()
	{
		this.Name = "Tiger Stance";
		this.Description = "Your hero has Windfury this turn.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Hero.HasWindfury = true;
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		this.Player.Hero.HasWindfury = false;
		this.TurnEndSubscription.Dispose();
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IDisposable TurnEndSubscription;
}
