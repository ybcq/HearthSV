using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LordMarrowgar : MinionCard
{
	public LordMarrowgar()
	{
		this.Name = "马洛加勋爵";
		this.Description = "Whenever this minion takes damage, deal 1 damage to all enemy minions.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 7;
		this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		List<Minion> enemyMinions = this.Player.Enemy.Minions.ToList<Minion>();
		foreach (Minion minion in enemyMinions)
		{
			yield return minion.Damage(null, 1);
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		foreach (Minion minion2 in enemyMinions)
		{
			yield return minion2.CheckDeath();
		}
		enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
