using System;
using System.Collections;
using UnityEngine;

public class YulontheJadeSerpent : MinionCard
{
	public YulontheJadeSerpent()
	{
		this.Name = "翡翠玉龙";
		this.Description = "Meditate: Set your current Health to the same as your opponent's.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 9;
		this.BaseAttack = 8;
		this.BaseHealth = 8;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator Meditate(Player player)
	{
		this.Player.Hero.CurrentHealth = this.Player.Enemy.Hero.CurrentHealth;
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IDisposable TurnEndSubscription;
}
