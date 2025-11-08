using System;
using System.Collections;
using UnityEngine;

public class TeronGorefiend : MinionCard
{
	public TeronGorefiend()
	{
		this.Name = "泰伦·戈尔";
		this.Description = "Battlecry and Inspire: Transform adjacent minions into 1/1 Ghoul.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 7;
		this.BaseAttack = 6;
		this.BaseHealth = 8;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		this.Mechanics.OnInspired.Add(new Func<InspireEvent, IEnumerator>(this.OnInspired));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character character)
	{
		foreach (Minion minion in this.Player.Minions)
		{
			if (minion.IsNextTo(this.Minion) && minion.Card.MinionType != MinionType.Totem)
			{
				ChargeTurnGhoul chargeTurnGhoul = new ChargeTurnGhoul();
				minion.TransformInto(chargeTurnGhoul);
				if (chargeTurnGhoul.Minion != null)
				{
					chargeTurnGhoul.Minion.HasCharge = false;
					chargeTurnGhoul.Minion.Mechanics.RemoveAll();
				}
			}
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnInspired(InspireEvent evt)
	{
		if (evt.Hero.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			foreach (Minion minion in this.Player.Minions)
			{
				if (minion.IsNextTo(this.Minion) && minion.Card.MinionType != MinionType.Totem)
				{
					ChargeTurnGhoul chargeTurnGhoul = new ChargeTurnGhoul();
					minion.TransformInto(chargeTurnGhoul);
					if (chargeTurnGhoul.Minion != null)
					{
						chargeTurnGhoul.Minion.HasCharge = false;
						chargeTurnGhoul.Minion.Mechanics.RemoveAll();
					}
				}
			}
			yield return new WaitForSeconds(0.25f);
			yield break;
		}
		yield break;
	}
}
