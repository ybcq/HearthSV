using System;
using System.Collections;
using UnityEngine;

public class HeWhoWatches : MinionCard
{
	public HeWhoWatches()
	{
		this.Name = "He Who Watches";
		this.Description = "Can't attack. Evasion. Whenever an enemy attack is Evaded, deal 3 damage to the attacker.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.CantAttack = true;
		this.IsEvasive = true;
		this.Mechanics.OnMinionEvade.Add(new Func<MinionEvadeEvent, IEnumerator>(this.OnMinionEvade));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionEvade(MinionEvadeEvent evt)
	{
		if (evt.Attacker.IsEnemyOf(this.Minion) && evt.Attacker.IsAlive())
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			yield return evt.Attacker.Damage(null, 3);
			yield return evt.Attacker.CheckDeath();
		}
		yield break;
	}

	public IEnumerator OnHeroEvade(HeroEvadeEvent evt)
	{
		if (evt.Attacker.IsEnemyOf(this.Minion) && evt.Attacker.IsAlive())
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			yield return evt.Attacker.Damage(null, 3);
			yield return evt.Attacker.CheckDeath();
		}
		yield break;
	}
}
