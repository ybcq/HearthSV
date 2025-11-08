using System;
using System.Collections;
using UnityEngine;

public class WrathfinMyrmidon : MinionCard
{
	public WrathfinMyrmidon()
	{
		this.Name = "Wrathfin Myrmidon";
		this.Description = "Whenever an enemy minion takes damage, increase it by 1.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 5;
		this.Mechanics.OnMinionPreDamage.Add(new Func<MinionPreDamageEvent, IEnumerator>(this.OnMinionPreDamage));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionPreDamage(MinionPreDamageEvent evt)
	{
		if (evt.Minion.IsEnemyOf(this.Minion))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			evt.DamageAmount++;
		}
		yield break;
	}
}
