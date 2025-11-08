using System;
using System.Collections;
using UnityEngine;

public class DemonicWards : SpellCard
{
	public DemonicWards()
	{
		this.Name = "Demonic Wards";
		this.Description = "Give a minion \"Can't be damaged by spells or Hero Powers.\"";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		minion.Mechanics.OnPreDamage.Add(new Func<MinionPreDamageEvent, IEnumerator>(this.OnPreDamage));
		yield break;
	}

	public IEnumerator OnPreDamage(MinionPreDamageEvent evt)
	{
		if (evt.Attacker == null)
		{
			evt.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			evt.DamageAmount = 0;
		}
		yield break;
	}
}
