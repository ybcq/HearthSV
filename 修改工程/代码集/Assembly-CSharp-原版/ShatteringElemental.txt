using System;
using System.Collections;
using UnityEngine;

public class ShatteringElemental : MinionCard
{
	public ShatteringElemental()
	{
		this.Name = "Shattering Elemental";
		this.Description = "Whenever this minion takes damage, summon a 1/1 Shardling.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		if (evt.DamageAmount > 0)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			yield return this.Player.SummonMinion(new Shardling());
		}
		yield break;
	}
}
