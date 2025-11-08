using System;
using System.Collections;
using UnityEngine;

public class GoldshirePatrol : MinionCard
{
	public GoldshirePatrol()
	{
		this.Name = "Goldshire Patrol";
		this.Description = "Taunt. Battlecry: Summon another Goldshire Patrol.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.HasTaunt = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.SummonMinion(new GoldshirePatrol(), this.Minion.GetPosition() + 1);
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
