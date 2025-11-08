using System;
using System.Collections;
using UnityEngine;

public class SnowLeopard : MinionCard
{
	public SnowLeopard()
	{
		this.Name = "盲眼的利萨特";
		this.Description = "Charge. Inaccurate. At the end of your turn, transform this minion into Leotheras Unleashed.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 7;
		this.BaseAttack = 5;
		this.BaseHealth = 7;
		this.HasCharge = true;
		this.IsInaccurate = true;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Minion.TransformInto(new SpitefulWraith());
		}
		yield break;
	}
}
