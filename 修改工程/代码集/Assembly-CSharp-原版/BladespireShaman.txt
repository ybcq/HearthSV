using System;
using System.Collections;
using UnityEngine;

public class BladespireShaman : MinionCard
{
	public BladespireShaman()
	{
		this.Name = "Bladespire Shaman";
		this.Description = "Battlecry: Summon a 1/1 Fire Nova Totem.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.SummonMinion(new FireNovaTotem());
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
