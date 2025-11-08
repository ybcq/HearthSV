using System;
using System.Collections;
using UnityEngine;

public class CoilskarGeneral : MinionCard
{
	public CoilskarGeneral()
	{
		this.Name = "疯狂的刽子手";
		this.Description = "Entry Song: Inflicts 2 damage to your main battler.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Vampire;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Hero.Controller, 2);
		yield return this.Player.Hero.Damage(null, 2);
		yield return this.Player.Hero.CheckDeath();
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
