using System;
using System.Collections;
using UnityEngine;

public class KoboldTunneler : MinionCard
{
	public KoboldTunneler()
	{
		this.Name = "ÉÌ··ÔªËØ";
		this.Description = "When you sell this, add a 2/2 Elemental to your hand.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.Mechanics.OnHeroPreDamage.Add(new Func<HeroPreDamageEvent, IEnumerator>(this.OnHeroPreDamage));
		base.InitializeMinion();
	}

	public IEnumerator OnHeroPreDamage(HeroPreDamageEvent evt)
	{
		if (evt.Attacker == this.Minion)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			this.Minion.Player.AddCardToHand(new ShatteringElemental());
			//this.Minion.Mechanics.Deathrattle = null;
			this.Minion.AddAuraAttackModifier(new Func<int, int>(this.ZeroModifier));
			this.Minion.AddAuraHealthModifier(new Func<int, int>(this.ZeroModifier));
		}
		yield break;
	}

	// Token: 0x06001BE9 RID: 7145
	public int ZeroModifier(int value)
	{
		return 0;
	}
}
