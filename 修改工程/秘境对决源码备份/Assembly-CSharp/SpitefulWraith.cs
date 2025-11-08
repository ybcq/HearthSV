using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpitefulWraith : MinionCard
{
	public SpitefulWraith()
	{
		this.Name = "纳兹夏尔女士";
		this.Description = "At the end of your turn, replace all other minions with new ones of the same Cost.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 10;
		this.BaseAttack = 10;
		this.BaseHealth = 10;
		this.HasCleave = true;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			using (List<Minion>.Enumerator enumerator = GameManager.Instance.GetAllMinions().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Minion minion = enumerator.Current;
					if (minion != this.Minion && minion.Card.MinionType != MinionType.Totem)
					{
						MinionCard minionCard = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
						where m.BaseCost == minion.Card.BaseCost
						select m).ToList<MinionCard>());
						if (minionCard != null)
						{
							minion.TransformInto(minionCard);
						}
					}
				}
			}
			MinionCard minionCard2 = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
			where m.BaseCost == this.Minion.Card.BaseCost
			select m).ToList<MinionCard>());
			this.Minion.TransformInto(minionCard2);
			yield break;
		}
		yield break;
	}
}
