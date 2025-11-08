using System;
using System.Collections;
using UnityEngine;

public class BlindlightRazorjaw : MinionCard
{
	public BlindlightRazorjaw()
	{
		this.Name = "龙人施法者";
		this.Description = "Battlecry: Summon two 2/2 Blindlight Razorjaws.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 5;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		int position = this.Minion.GetPosition();
		yield return this.Player.SummonMinion(new BlindlightRazorjaw
		{
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		}, position + 1);
		yield return this.Player.SummonMinion(new BlindlightRazorjaw
		{
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		}, position);
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
