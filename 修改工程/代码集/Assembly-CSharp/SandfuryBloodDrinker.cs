using System;
using System.Collections;
using UnityEngine;

public class SandfuryBloodDrinker : MinionCard
{
	public SandfuryBloodDrinker()
	{
		this.Name = "沙怒血饮";
		this.Description = "Whenever this minion deals damage, restore that much Health to it.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 6;
		this.Mechanics.OnMinionDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnMinionDamaged));
		this.Mechanics.OnHeroDamaged.Add(new Func<HeroDamagedEvent, IEnumerator>(this.OnHeroDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionDamaged(MinionDamagedEvent evt)
	{
		if (evt.Attacker == this.Minion)
		{
			yield return this.OnDamageDealt(evt.DamageAmount);
		}
		yield break;
	}

	public IEnumerator OnHeroDamaged(HeroDamagedEvent evt)
	{
		if (evt.Attacker == this.Minion)
		{
			yield return this.OnDamageDealt(evt.DamageAmount);
		}
		yield break;
	}

	private IEnumerator OnDamageDealt(int amount)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		yield return this.Minion.Heal(amount);
		yield break;
	}
}
