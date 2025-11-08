using System;
using System.Collections;
using UnityEngine;

public class SpitefulWraith : MinionCard
{
	public SpitefulWraith()
	{
		this.Name = "解脱的利萨特";
		this.Description = "Cleave. At the end of your turn, transform this minion into Leotheras the Blind.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 7;
		this.BaseAttack = 5;
		this.BaseHealth = 7;
		this.HasCleave = true;
		this.Collectible = false;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Minion.TransformInto(new SnowLeopard());
		}
		yield break;
	}
}
