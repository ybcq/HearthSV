using System;
using System.Collections;
using UnityEngine;

public class SuntouchedWarrior : MinionCard
{
	public SuntouchedWarrior()
	{
		this.Name = "救赎者娜拉";
		this.Description = "Divine Shield. Taunt. After this minion survives damage, gain Divine Shield.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 6;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		this.HasDivineShield = true;
		this.BuDun = false;
		this.Mechanics.OnPreDamage.Add(new Func<MinionPreDamageEvent, IEnumerator>(this.OnPreDamage));
		this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		if (this.BuDun)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			this.Minion.HasDivineShield = true;
		}
		yield break;
	}

	public IEnumerator OnPreDamage(MinionPreDamageEvent evt)
	{
		if (!this.Minion.HasDivineShield)
		{
			this.BuDun = true;
		}
		else
		{
			this.BuDun = false;
		}
		yield break;
	}

	public bool BuDun;
}
