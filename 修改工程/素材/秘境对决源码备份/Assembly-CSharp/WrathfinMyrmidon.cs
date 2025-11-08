using System;
using System.Collections;
using UnityEngine;

public class WrathfinMyrmidon : MinionCard
{
	public WrathfinMyrmidon()
	{
		this.Name = "深渊巨兽";
		this.Description = "Entry Song: If revenge has been activated, inflict 5 damage to an enemy's entourage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Vampire;
		this.BaseCost = 7;
		this.BaseAttack = 5;
		this.BaseHealth = 6;
		this.BattlecryType = BattlecryType.EnemyMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return this.Player.Hero.CurrentHealth <= 10;
	}

	public IEnumerator Battlecry(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 5);
		yield return target.Damage(null, 5);
		yield return target.CheckDeath();
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && (target.IsEnemyOf(this.Player.Hero) || !target.IsStealth) && target.IsMinion() && target.As<Minion>().Card.MinionType != MinionType.Totem;
	}
}
