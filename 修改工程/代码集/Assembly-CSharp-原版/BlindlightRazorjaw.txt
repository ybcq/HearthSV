using System;
using System.Collections;
using UnityEngine;

public class BlindlightRazorjaw : MinionCard
{
	public BlindlightRazorjaw()
	{
		this.Name = "Blindlight Razorjaw";
		this.Description = "Battlecry: Summon two 1/1 Blindlight Razorjaws.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Murloc;
		this.BaseCost = 3;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		int position = this.Minion.GetPosition();
		yield return this.Player.SummonMinion(new BlindlightRazorjaw(), position + 1);
		yield return this.Player.SummonMinion(new BlindlightRazorjaw(), position);
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
