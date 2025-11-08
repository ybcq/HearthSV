using System;
using System.Collections;
using UnityEngine;

public class Murozond : MinionCard
{
	public Murozond()
	{
		this.Name = "复仇军铸甲师";
		this.Description = "At the start of each turn, gain 2 Amor.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.Golden = true;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		this.Player.Hero.CurrentArmor += 2;
		yield break;
	}
}
