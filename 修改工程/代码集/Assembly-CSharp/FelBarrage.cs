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
		this.Description = "Deal your cards damage randomly split among all enemies.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
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
			List<Character> characters = (from c in this.Player.Enemy.GetAllCharacters()
			where c.IsAlive()
			select c).ToList<Character>();
			Character randomCharacter = RNG.RandomItemFrom<Character>(characters);
			InterfaceManager.Instance.SpawnDamageSplatOn(randomCharacter.Controller, 1);
			yield return randomCharacter.Damage(null, 1);
			yield return randomCharacter.CheckDeath();
			yield return new WaitForSeconds(0.25f);
			randomCharacter = null;
			num = i;
			randomCharacter = null;
			randomCharacter = null;
		}
		yield break;
	}
}
