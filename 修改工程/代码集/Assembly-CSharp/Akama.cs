using System;
using System.Collections;
using UnityEngine;

public class Akama : MinionCard
{
	public Akama()
	{
		this.Name = "赤间";
		this.Description = "Battlecry: Transform adjacent minions into 1/1 Shades of Akama.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 6;
		this.BaseHealth = 6;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		foreach (Minion minion in this.Player.Minions)
		{
			if (minion.IsNextTo(this.Minion))
			{
				minion.TransformInto(new ShadeofAkama());
			}
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
