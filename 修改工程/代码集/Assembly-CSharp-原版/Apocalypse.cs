using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Apocalypse : WeaponCard
{
	public Apocalypse()
	{
		this.Name = "Apocalypse";
		this.Description = "Whenever your hero attacks, trigger a random Deathrattle effect that triggered this game.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.BaseCost = 6;
		this.BaseAttack = 4;
		this.BaseDurability = 2;
		this.Mechanics.OnAttacked.Add(new Func<AttackedEvent, IEnumerator>(this.OnAttacked));
		base.InitializeWeapon();
	}

	public IEnumerator OnAttacked(AttackedEvent evt)
	{
		List<List<Func<Minion, IEnumerator>>> selfDeathrattles = (from m in this.Player.DeadMinions
		where m.Minion.Mechanics.HasDeathrattle()
		select m.Minion.Mechanics.Deathrattle.Events).Concat(from w in this.Player.DestroyedWeapons
		where w.Weapon.Mechanics.HasDeathrattle()
		select w.Weapon.Mechanics.Deathrattle.Events).ToList<List<Func<Minion, IEnumerator>>>();
		List<List<Func<Minion, IEnumerator>>> enemyDeathrattles = (from m in this.Player.Enemy.DeadMinions
		where m.Minion.Mechanics.HasDeathrattle()
		select m.Minion.Mechanics.Deathrattle.Events).Concat(from w in this.Player.DestroyedWeapons
		where w.Weapon.Mechanics.HasDeathrattle()
		select w.Weapon.Mechanics.Deathrattle.Events).ToList<List<Func<Minion, IEnumerator>>>();
		List<Func<Minion, IEnumerator>> allDeathrattles = new List<Func<Minion, IEnumerator>>();
		foreach (List<Func<Minion, IEnumerator>> second in selfDeathrattles.Concat(enemyDeathrattles))
		{
			allDeathrattles = allDeathrattles.Concat(second).ToList<Func<Minion, IEnumerator>>();
		}
		Func<Minion, IEnumerator> randomDeathrattle = RNG.RandomItemFrom<Func<Minion, IEnumerator>>(allDeathrattles);
		if (randomDeathrattle != null)
		{
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			BaseCard deathrattleCard = (BaseCard)Activator.CreateInstance(randomDeathrattle.Method.DeclaringType);
			yield return InterfaceManager.Instance.ShowNeutralCard(deathrattleCard);
			yield return randomDeathrattle(null);
		}
		yield break;
	}
}
