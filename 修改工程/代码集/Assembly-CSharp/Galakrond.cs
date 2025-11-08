using System;
using System.Collections;
using UnityEngine;

public class Galakrond : MinionCard
{
	public Galakrond()
	{
		this.Name = "迦拉克隆";
		this.Description = "Battlecry: Destroy ALL other Dragons and gain +3/+3 for each Dragon destroyed this way.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 10;
		this.BaseAttack = 9;
		this.BaseHealth = 9;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		this.DestroyedDragons = 0;
		foreach (Minion minion in this.Player.Minions)
		{
			if (minion != this.Minion && minion.Card.MinionType == MinionType.Dragon)
			{
				yield return minion.Destroy();
				this.DestroyedDragons++;
			}
		}
		if (this.DestroyedDragons > 0)
		{
			yield return new WaitForSeconds(0.25f);
			this.Minion.AddAttackModifier(new Func<int, int>(this.GalakrondModifier));
			this.Minion.AddHealthModifier(new Func<int, int>(this.GalakrondModifier));
			this.Minion.CurrentHealth += this.DestroyedDragons * 3;
		}
		yield break;
	}

	private int GalakrondModifier(int value)
	{
		return value + this.DestroyedDragons * 3;
	}

	private int DestroyedDragons;
}
