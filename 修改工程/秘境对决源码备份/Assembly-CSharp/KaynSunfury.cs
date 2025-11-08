using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaynSunfury : MinionCard
{
	public KaynSunfury()
	{
		this.Name = "风神";
		this.Description = "Battlecry and at the start of your turn, Give your other minions +1 Attack.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 1;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		using (List<Minion>.Enumerator enumerator = this.Player.Minions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				if (minion.Card.MinionType != MinionType.Totem)
				{
					minion.AddAttackModifier(new Func<int, int>(this.FengShenModifier));
				}
			}
			yield break;
		}
		yield break;
	}

	public int FengShenModifier(int attack)
	{
		return attack + 1;
	}

	private IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			foreach (Minion minion in this.Player.Minions)
			{
				minion.AddAttackModifier(new Func<int, int>(this.FengShenModifier));
			}
			yield break;
		}
		yield break;
	}
}
