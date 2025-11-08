using System;
using System.Collections;
using UnityEngine;

public class Helboar : MinionCard
{
	public Helboar()
	{
		this.Name = "赫尔瓦尔";
		this.Description = string.Empty;
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
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
