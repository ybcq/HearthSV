using System;
using System.Collections;
using UnityEngine;

public class GreenpawFurbolg : MinionCard
{
	public GreenpawFurbolg()
	{
		this.Name = "罗密欧";
		this.Description = "At the end of your turn, restore 3 health to your hero.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseHealth = 4;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		yield return this.Player.Hero.Heal(3);
		yield break;
	}
}
