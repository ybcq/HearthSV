using System;
using System.Collections;
using UnityEngine;

public class LeiShen : MinionCard
{
	public LeiShen()
	{
		this.Name = "雷神";
		this.Description = "Windfury. Whenever a friendly minion attacks, give your other minions +1 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 8;
		this.BaseAttack = 2;
		this.BaseHealth = 10;
		this.HasWindfury = true;
		this.Mechanics.OnMinionAttacked.Add(new Func<MinionAttackedEvent, IEnumerator>(this.OnMinionAttacked));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionAttacked(MinionAttackedEvent minionAttackedEvent)
	{
		if (minionAttackedEvent.Minion.IsFriendlyOf(this.Minion))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			foreach (Minion minion in this.Player.Minions)
			{
				if (minion != this.Minion)
				{
					minion.AddAttackModifier(new Func<int, int>(this.ApplyLeiShenModifier));
				}
			}
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public int ApplyLeiShenModifier(int attack)
	{
		return attack + 1;
	}
}
