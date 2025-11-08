using System;
using System.Collections;
using System.Collections.Generic;

public class ZenMaster : MinionCard
{
	public ZenMaster()
	{
		this.Name = "禅师";
		this.Description = "Meditate: Give your minions +1/+1.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator Meditate(Player player)
	{
		using (List<Minion>.Enumerator enumerator = player.Minions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				if (minion.Card.MinionType != MinionType.Totem)
				{
					minion.AddAttackModifier(new Func<int, int>(this.MeditateModifier));
					minion.CurrentHealth++;
					minion.AddHealthModifier(new Func<int, int>(this.MeditateModifier));
				}
			}
			yield break;
		}
		yield break;
	}

	public int MeditateModifier(int value)
	{
		return value + 1;
	}

	public Player MeditatePlayer;
}
