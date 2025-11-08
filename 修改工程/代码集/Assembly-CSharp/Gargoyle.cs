using System;
using System.Collections;
using UnityEngine;

public class Gargoyle : MinionCard
{
	public Gargoyle()
	{
		this.Name = "石像鬼";
		this.Description = "At the end of each turn, deal 1 damage to this minion's target.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (this.Target != null && this.Target.IsAlive())
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			InterfaceManager.Instance.SpawnDamageSplatOn(this.Target.Controller, 1);
			yield return this.Target.Damage(null, 1);
			yield return this.Target.CheckDeath();
		}
		yield break;
	}

	public Character Target;
}
