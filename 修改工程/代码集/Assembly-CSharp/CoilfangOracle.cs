using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CoilfangOracle : MinionCard
{
	public CoilfangOracle()
	{
		this.Name = "Coilfang Oracle";
		this.Description = "Whenever you cast a spell, reduce the Cost of Nagas in your hand and deck by (1).";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 4;
		this.Mechanics.OnSpellCasted.Add(new Func<SpellCastedEvent, IEnumerator>(this.OnSpellCasted));
		base.InitializeMinion();
	}

	public IEnumerator OnSpellCasted(SpellCastedEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			foreach (MinionCard minionCard in this.Player.Hand.OfType<MinionCard>())
			{
				if (minionCard.MinionType == MinionType.Naga)
				{
					minionCard.AddCostModifier(new Func<int, int>(this.CoilfangModifier));
				}
			}
			foreach (MinionCard minionCard2 in this.Player.Deck.OfType<MinionCard>())
			{
				if (minionCard2.MinionType == MinionType.Naga)
				{
					minionCard2.AddCostModifier(new Func<int, int>(this.CoilfangModifier));
				}
			}
			this.CostModifier++;
		}
		yield break;
	}

	public int CoilfangModifier(int cost)
	{
		return cost - this.CostModifier;
	}

	public int CostModifier;
}
