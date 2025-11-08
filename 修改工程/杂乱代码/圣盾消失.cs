using System;
using System.Collections;
using UnityEngine;

public class Brightwing : MinionCard
{
	public Brightwing()
	{
		this.Name = "¡˙»À÷¥––’ﬂ";
		this.Description = "After a friendly minion loses Divine Shield, gain +2/+2.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 6;
		this.BaseAttack = 3;
		this.BaseHealth = 6;
		this.IsStealth = true;
		this.Mechanics.OnMinionPreDamage.Add(new Func<MinionPreDamageEvent, IEnumerator>(this.OnMinionPreDamage));
		this.Mechanics.OnMinionDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnMinionDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionPreDamage(MinionPreDamageEvent evt)
	{
		if (evt.Minion.IsFriendlyOf(this.Player.Hero) && evt.Minion.HasDivineShield)
		{
			this.MinionHasDivineShield = true;
		}
		yield break;
	}

	public IEnumerator OnMinionDamaged(MinionDamagedEvent evt)
	{
		if (evt.Minion.IsFriendlyOf(this.Player.Hero) && !evt.Minion.HasDivineShield && this.MinionHasDivineShield)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			this.Minion.AddAttackModifier(new Func<int, int>(this.BrightwingModifier));
			this.Minion.AddHealthModifier(new Func<int, int>(this.BrightwingModifier));
			this.Minion.CurrentHealth += 2;
			this.MinionHasDivineShield = false;
		}
		yield break;
	}

	public int BrightwingModifier(int value)
	{
		return value + 2;
	}

	public bool MinionHasDivineShield;
}
