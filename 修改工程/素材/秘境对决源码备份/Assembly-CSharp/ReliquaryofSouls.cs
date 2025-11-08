using System;
using System.Collections;
using UnityEngine;

public class ReliquaryofSouls : MinionCard
{
	public ReliquaryofSouls()
	{
		this.Name = "孚里埃";
		this.Description = "At the end of your turn, deal 1 damage to your Enemy's Hero and draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.Vampire;
		this.BaseCost = 8;
		this.BaseAttack = 7;
		this.BaseHealth = 7;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			yield return this.Player.Draw(null);
			InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 1);
			yield return this.Player.Enemy.Hero.Damage(null, 1);
			yield return this.Player.Enemy.Hero.CheckDeath();
		}
		yield break;
	}
}
