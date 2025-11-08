using System;
using System.Collections;
using UnityEngine;

public class ShadowsongAssassin : MinionCard
{
	public ShadowsongAssassin()
	{
		this.Name = "Shadowsong Assassin";
		this.Description = "Stealth. At the end of each turn, gain +1 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 5;
		this.IsStealth = true;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		this.Minion.AddAttackModifier(new Func<int, int>(this.ShadowAssassinModifier));
		yield break;
	}

	public int ShadowAssassinModifier(int attack)
	{
		return attack + 1;
	}
}
