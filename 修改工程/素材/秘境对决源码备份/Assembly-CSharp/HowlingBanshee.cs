using System;
using System.Collections;
using UnityEngine;

public class HowlingBanshee : MinionCard
{
	public HowlingBanshee()
	{
		this.Name = "尖叫女妖";
		this.Description = "Battlecry: Give a friendly minion Spellshield.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.FriendlyMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return this.Player.Minions.Count > 0;
	}

	public IEnumerator Battlecry(Character target)
	{
		target.As<Minion>().HasSpellshield = true;
		target.Controller.UpdateSprites();
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
