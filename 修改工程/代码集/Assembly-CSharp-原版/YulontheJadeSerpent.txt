using System;
using System.Collections;
using UnityEngine;

public class YulontheJadeSerpent : MinionCard
{
	public YulontheJadeSerpent()
	{
		this.Name = "Yu'lon the Jade Serpent";
		this.Description = "Battlecry: Set your current Health to the same as your opponent's.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 9;
		this.BaseAttack = 8;
		this.BaseHealth = 8;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		this.Player.Hero.CurrentHealth = this.Player.Enemy.Hero.CurrentHealth;
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IDisposable TurnEndSubscription;
}
