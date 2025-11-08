using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeofAkama : MinionCard
{
	public ShadeofAkama()
	{
		this.Name = "瘟疫使者诺斯";
		this.Description = "Whenever an enemy minion dies, summon a 1/1 Skeleton and give your other minions +1/+1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 9;
		this.BaseAttack = 2;
		this.BaseHealth = 5;
		this.Mechanics.OnMinionDied.Add(new Func<MinionDiedEvent, IEnumerator>(this.OnMinionDied));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionDied(MinionDiedEvent evt)
	{
		if (evt.Minion.IsEnemyOf(this.Minion))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			SkeletonCommander minionCard = new SkeletonCommander();
			yield return this.Player.SummonMinion(minionCard);
			if (minionCard.Minion != null)
			{
				minionCard.Minion.Mechanics.RemoveAll();
			}
			using (List<Minion>.Enumerator enumerator = GameManager.Instance.GetAllMinions().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Minion minion = enumerator.Current;
					if (minion.IsFriendlyOf(this.Minion) && minion != this.Minion)
					{
						minion.AddAttackModifier(new Func<int, int>(this.ApplyModifier));
						minion.AddHealthModifier(new Func<int, int>(this.ApplyModifier));
						minion.CurrentHealth++;
					}
				}
				yield break;
			}
		}
		yield break;
	}

	public int ApplyModifier(int value)
	{
		return value + 1;
	}
}
