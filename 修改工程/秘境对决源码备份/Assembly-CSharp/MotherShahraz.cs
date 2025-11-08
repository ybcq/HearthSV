using System;
using System.Collections;
using UnityEngine;

public class MotherShahraz : MinionCard
{
	public MotherShahraz()
	{
		this.Name = "魅惑的吸血鬼";
		this.Description = "Whenever this minion deals damage, restore that much Health to your Hero.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Vampire;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 3;
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
		yield return this.Player.Hero.Heal(amount);
		yield break;
	}
}
