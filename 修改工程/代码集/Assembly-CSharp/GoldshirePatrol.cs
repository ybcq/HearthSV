using System;
using System.Collections;
using UnityEngine;

public class GoldshirePatrol : MinionCard
{
	public GoldshirePatrol()
	{
		this.Name = "萨隆苦囚";
		this.Description = "Taunt. Battlecry: Summon another Goldshire Patrol.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.SummonMinion(new GoldshirePatrol
		{
			BaseAttack = base.CurrentAttack,
			BaseHealth = this.CurrentHealth,
			CurrentHealth = this.CurrentHealth
		}, this.Minion.GetPosition() + 1);
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
