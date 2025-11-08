using System;
using System.Collections;
using UnityEngine;

public class CrushridgeMauler : MinionCard
{
	public CrushridgeMauler()
	{
		this.Name = "鬼灵骑兵";
		this.Description = "At the beginning of your turn, deal 1 damage to your hero.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 0;
		this.BaseHealth = 6;
		this.Collectible = false;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Hero.Controller, 1);
			yield return this.Player.Hero.Damage(null, 1);
			yield return this.Player.Hero.CheckDeath();
		}
		yield break;
	}
}
