using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class IrradiatedGnome : MinionCard
{
	public IrradiatedGnome()
	{
		this.Name = "被感染的豪猪人";
		this.Description = "Whenever this minion takes damage, deal 1 damage to a random enemy minion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 7;
		this.DamagedSubscription = this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where m.CurrentAttack >= 0
		select m).ToList<Minion>();
		if (list.Count > 0)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			Character randomTarget = RNG.RandomItemFrom<Minion>(list);
			InterfaceManager.Instance.SpawnDamageSplatOn(randomTarget.Controller, 1);
			yield return randomTarget.Damage(null, 1);
			yield return randomTarget.CheckDeath();
			randomTarget = null;
			randomTarget = null;
			randomTarget = null;
		}
		yield break;
	}

	public IDisposable DamagedSubscription;
}
