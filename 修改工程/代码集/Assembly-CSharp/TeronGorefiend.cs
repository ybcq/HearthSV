using System;
using System.Collections;
using UnityEngine;

public class TeronGorefiend : MinionCard
{
	public TeronGorefiend()
	{
		this.Name = "泰伦·戈尔";
		this.Description = "Battlecry and Deathrattle: Summon two 1/1 Ghouls.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 7;
		this.BaseAttack = 6;
		this.BaseHealth = 6;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character character)
	{
		int teronPosition = this.Minion.GetPosition();
		yield return this.Player.SummonMinion(new ChargeTurnGhoul(), teronPosition + 1);
		yield return this.Player.SummonMinion(new ChargeTurnGhoul(), teronPosition);
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator Deathrattle(Minion self)
	{
		int teronPosition = this.Minion.GetPosition();
		if (teronPosition != -1)
		{
			yield return self.Player.SummonMinion(new ChargeTurnGhoul(), teronPosition + 1);
			yield return self.Player.SummonMinion(new ChargeTurnGhoul(), teronPosition);
		}
		else
		{
			yield return self.Player.SummonMinion(new ChargeTurnGhoul());
			yield return self.Player.SummonMinion(new ChargeTurnGhoul());
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
