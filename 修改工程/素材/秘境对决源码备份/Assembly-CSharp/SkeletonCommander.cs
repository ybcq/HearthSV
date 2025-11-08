using System;
using System.Collections;
using UnityEngine;

public class SkeletonCommander : MinionCard
{
	public SkeletonCommander()
	{
		this.Name = "骷髅指挥官";
		this.Description = "Deathrattle: The next Undead you play gets +1/+1.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		this.MinionPlayedSubscription = EventManager.Instance.MinionPlayedHandler.Add((MinionPlayedEvent x) => this.OnMinionPlayed(x, self));
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnMinionPlayed(MinionPlayedEvent minionPlayedEvent, Minion minion)
	{
		if (minionPlayedEvent.Player == minion.Player && minionPlayedEvent.Minion.Card.MinionType == MinionType.Undead)
		{
			minionPlayedEvent.Minion.AddAttackModifier(new Func<int, int>(this.SkeletonCommanderModifier));
			minionPlayedEvent.Minion.CurrentHealth++;
			minionPlayedEvent.Minion.AddHealthModifier(new Func<int, int>(this.SkeletonCommanderModifier));
			this.MinionPlayedSubscription.Dispose();
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public int SkeletonCommanderModifier(int value)
	{
		return value + 1;
	}

	public IDisposable MinionPlayedSubscription;
}
