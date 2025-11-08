using System;
using System.Collections;
using UnityEngine;

public class FleshColossus : MinionCard
{
	public FleshColossus()
	{
		this.Name = "腐肉巨像";
		this.Description = "Whenever an Undead dies, gain +1/+1.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 7;
		this.Mechanics.OnMinionDied.Add(new Func<MinionDiedEvent, IEnumerator>(this.OnMinionDied));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionDied(MinionDiedEvent minionDiedEvent)
	{
		if (minionDiedEvent.Minion.Card.MinionType == MinionType.Undead && minionDiedEvent.Minion != this.Minion)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Minion.AddAttackModifier(new Func<int, int>(this.MinionDiedModifier));
			this.Minion.CurrentHealth++;
			this.Minion.AddHealthModifier(new Func<int, int>(this.MinionDiedModifier));
		}
		yield break;
	}

	public int MinionDiedModifier(int value)
	{
		return value + 1;
	}
}
