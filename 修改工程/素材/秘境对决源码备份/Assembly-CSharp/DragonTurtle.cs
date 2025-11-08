using System;
using System.Collections;
using UnityEngine;

public class DragonTurtle : MinionCard
{
	public DragonTurtle()
	{
		this.Name = "龙龟";
		this.Description = "Meditate: All u Minions Get Taunt.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 6;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator Meditate(Player player)
	{
		foreach (Minion minion in player.Minions)
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				minion.HasTaunt = true;
			}
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
