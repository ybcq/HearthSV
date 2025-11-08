using System;
using System.Collections;
using UnityEngine;

public class EbonBladeVindicator : MinionCard
{
	public EbonBladeVindicator()
	{
		this.Name = "乌木之刃辩护者";
		this.Description = "Whenever a friendly minion dies, gain +2 Attack.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseHealth = 6;
		this.Mechanics.OnMinionDied.Add(new Func<MinionDiedEvent, IEnumerator>(this.OnMinionDied));
		base.InitializeMinion();
	}

	private IEnumerator OnMinionDied(MinionDiedEvent minionDiedEvent)
	{
		if (minionDiedEvent.Minion.IsFriendlyOf(this.Minion))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			this.Minion.AddAttackModifier(new Func<int, int>(this.FriendlyMinionDiedModifier));
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	private int FriendlyMinionDiedModifier(int attack)
	{
		return attack + 2;
	}
}
