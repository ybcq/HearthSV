using System;
using System.Collections;
using System.Collections.Generic;

public class AltruistheSufferer : MinionCard
{
	public AltruistheSufferer()
	{
		this.Name = "睿智指挥官";
		this.Description = "Battlecry: Give your other minions +1/+1. ";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.RoyalGuard;
		this.BaseCost = 6;
		this.BaseAttack = 4;
		this.BaseHealth = 6;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		using (List<Minion>.Enumerator enumerator = this.Player.Minions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				if (minion != this.Minion && minion.Card.MinionType != MinionType.Totem)
				{
					minion.CurrentHealth++;
					minion.AddHealthModifier(new Func<int, int>(this.AltruisModifier));
					minion.AddAttackModifier(new Func<int, int>(this.AltruisModifier));
				}
			}
			yield break;
		}
		yield break;
	}

	public int AltruisModifier(int health)
	{
		return health + 1;
	}
}
