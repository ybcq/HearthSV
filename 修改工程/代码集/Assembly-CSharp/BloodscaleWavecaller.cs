using System;
using System.Collections;
using UnityEngine;

public class BloodscaleWavecaller : MinionCard
{
	public BloodscaleWavecaller()
	{
		this.Name = "Bloodscale Wavecaller";
		this.Description = "Whenever a minion dies, gain Spell Damage +1.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.Mechanics.OnMinionDied.Add(new Func<MinionDiedEvent, IEnumerator>(this.OnMinionDied));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionDied(MinionDiedEvent evt)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		this.Minion.SpellPower++;
		yield break;
	}
}
