using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HiredThug : MinionCard
{
	public HiredThug()
	{
		this.Name = "溅射焰团";
		this.Description = "Deathrattle: Deal 1 damage to a random enemy.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 1;
		this.BaseAttack = 2;
		this.BaseHealth = 1;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.25f);
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where m.CurrentAttack >= 1 && m.Card.MinionType == MinionType.Biol
		select m).ToList<Minion>();
		if (list.Count > 0)
		{
			Character randomTarget = RNG.RandomItemFrom<Minion>(list);
			InterfaceManager.Instance.SpawnDamageSplatOn(randomTarget.Controller, 1);
			yield return randomTarget.Damage(null, 1);
			yield return randomTarget.CheckDeath();
			randomTarget = null;
		}
		else
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 1);
			yield return this.Player.Enemy.Hero.Damage(null, 1);
			yield return this.Player.Enemy.Hero.CheckDeath();
		}
		yield break;
	}
}
