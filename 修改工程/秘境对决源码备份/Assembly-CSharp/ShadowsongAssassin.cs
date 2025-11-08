using System;
using System.Collections;
using UnityEngine;

public class ShadowsongAssassin : MinionCard
{
	public ShadowsongAssassin()
	{
		this.Name = "灰烬狂热者";
		this.Description = "At the start of your turn, deal 1 damage to your opponent.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent turnEvent)
	{
		if (turnEvent.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 1);
			this.Player.Enemy.Hero.Damage(null, 1);
			this.Player.Enemy.Hero.CheckDeath();
		}
		yield break;
	}
}
