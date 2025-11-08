using System;
using System.Collections;
using UnityEngine;

public class ShatteringElemental : MinionCard
{
	public ShatteringElemental()
	{
		this.Name = "加尔";
		this.Description = "Whenever this minion takes damage, summon a 2/3 Element with Taunt.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 8;
		this.Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		if (evt.DamageAmount > 0)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			Kobold Element = new Kobold
			{
				BaseAttack = 2,
				BaseHealth = 3,
				CurrentHealth = 3
			};
			yield return this.Player.SummonMinion(Element);
			if (Element.Minion != null)
			{
				Element.Minion.Silence();
				Element.Minion.HasTaunt = true;
			}
			Element = null;
		}
		yield break;
	}
}
