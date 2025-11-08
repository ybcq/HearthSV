using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FelBarrage : SpellCard
{
	public FelBarrage()
	{
		this.Name = "森林的意志";
		this.Description = "Deal the same damage randomly split among all enemies as your cards.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int iterations = this.Player.Hand.Count + this.Player.GetSpellPower();
		Debugger.Log(iterations + " iterations");
		int num;
		for (int i = 0; i < iterations; i = num + 1)
		{
			List<Minion> characters = (from c in this.Player.Enemy.Minions
			where c.IsAlive() && c.Card.MinionType != MinionType.Totem
			select c).ToList<Minion>();
			Minion randomCharacter = RNG.RandomItemFrom<Minion>(characters);
			if (randomCharacter != null)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(randomCharacter.Controller, 1);
				yield return randomCharacter.Damage(null, 1);
				yield return randomCharacter.CheckDeath();
				yield return new WaitForSeconds(0.25f);
			}
			num = i;
			randomCharacter = null;
		}
		yield break;
	}
}
