using System;
using System.Collections;
using UnityEngine;

public class Helboar : MinionCard
{
	public Helboar()
	{
		this.Name = "Helboar";
		this.Description = "Whenever this minion takes damage, double its Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.25f);
		this.Minion.AddAttackModifier(new Func<int, int>(this.HelboarModifier));
		yield break;
	}

	private int HelboarModifier(int attack)
	{
		return attack * 2;
	}
}
