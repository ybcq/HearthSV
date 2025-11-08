using System;
using System.Collections;
using UnityEngine;

public class Helboar : MinionCard
{
	public Helboar()
	{
		this.Name = "钢铁卫士";
		this.Description = "This minion can only take 1 damage at a time.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.HasDivineShield = true;
		this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		this.Minion.HasDivineShield = true;
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.25f);
		this.Minion.CurrentHealth--;
		yield break;
	}
}
