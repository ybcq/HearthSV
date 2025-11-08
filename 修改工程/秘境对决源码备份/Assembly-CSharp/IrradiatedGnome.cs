using System;
using System.Collections;
using UnityEngine;

public class IrradiatedGnome : MinionCard
{
	public IrradiatedGnome()
	{
		this.Name = "塞瑞亚战巫";
		this.Description = "Taunt. Whenever this creature takes damage, restore 1 life.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 4;
		this.HasTaunt = true;
		this.DamagedSubscription = this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		if (evt.Minion == this.Minion)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			this.Minion.Heal(1);
		}
		yield break;
	}

	public IDisposable DamagedSubscription;
}
