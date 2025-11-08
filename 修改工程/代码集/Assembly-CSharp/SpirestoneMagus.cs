using System;
using System.Collections;
using UnityEngine;

public class SpirestoneMagus : MinionCard
{
	public SpirestoneMagus()
	{
		this.Name = "螺旋石魔术师";
		this.Description = "Battlecry: Transform a minion into a 1/1 Ghoul.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.General;
		this.BaseCost = 8;
		this.BaseAttack = 6;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.AllMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		target.As<Minion>().TransformInto(new ChargeTurnGhoul());
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
