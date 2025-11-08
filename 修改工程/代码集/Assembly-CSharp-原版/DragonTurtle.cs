using System;
using System.Collections;
using UnityEngine;

public class DragonTurtle : MinionCard
{
	public DragonTurtle()
	{
		this.Name = "Dragon Turtle";
		this.Description = "Meditate: Gain +1/+1 and Taunt.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 6;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator Meditate(Player player)
	{
		if (this.Minion != null && this.Minion.IsAlive())
		{
			this.Minion.AddAttackModifier(new Func<int, int>(this.MeditateModifier));
			this.Minion.CurrentHealth++;
			this.Minion.AddHealthModifier(new Func<int, int>(this.MeditateModifier));
			this.Minion.HasTaunt = true;
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public int MeditateModifier(int attack)
	{
		return attack + 1;
	}
}
