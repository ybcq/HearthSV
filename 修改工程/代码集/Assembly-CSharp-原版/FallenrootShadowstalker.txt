using System;
using System.Collections;
using UnityEngine;

public class FallenrootShadowstalker : MinionCard
{
	public FallenrootShadowstalker()
	{
		this.Name = "Fallenroot Shadowstalker";
		this.Description = "Stealth. Whenever an enemy minion attacks, give it -1 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.IsStealth = true;
		this.Mechanics.OnMinionAttacked.Add(new Func<MinionAttackedEvent, IEnumerator>(this.OnMinionAttacked));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionAttacked(MinionAttackedEvent evt)
	{
		if (evt.Minion.IsEnemyOf(this.Minion))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			evt.Minion.AddAttackModifier(new Func<int, int>(this.ShadowstalkerModifier));
		}
		yield break;
	}

	public int ShadowstalkerModifier(int attack)
	{
		return attack - 1;
	}
}
