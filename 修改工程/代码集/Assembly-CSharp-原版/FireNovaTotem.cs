using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireNovaTotem : MinionCard
{
	public FireNovaTotem()
	{
		this.Name = "Fire Nova Totem";
		this.Description = "At the start of your turn, deal 1 damage to all enemies.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Totem;
		this.Collectible = false;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			List<Minion> aliveMinions = (from m in this.Player.Enemy.Minions
			where m.IsAlive()
			select m).ToList<Minion>();
			foreach (Minion minion in aliveMinions)
			{
				yield return minion.Damage(null, 1);
			}
			foreach (Minion minion2 in aliveMinions)
			{
				yield return minion2.CheckDeath();
			}
		}
		yield break;
	}
}
