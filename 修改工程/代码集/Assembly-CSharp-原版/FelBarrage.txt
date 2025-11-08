using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FelBarrage : SpellCard
{
	public FelBarrage()
	{
		this.Name = "Fel Barrage";
		this.Description = "Deal 4-5 damage randomly split among all enemies.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int iterations = RNG.RandomInteger(4, 5) + this.Player.GetSpellPower();
		Debugger.Log(iterations + " iterations");
		for (int i = 0; i < iterations; i++)
		{
			List<Character> availableCharacters = (from c in this.Player.Enemy.GetAllCharacters()
			where c.IsAlive()
			select c).ToList<Character>();
			Character randomCharacter = RNG.RandomItemFrom<Character>(availableCharacters);
			yield return randomCharacter.Damage(null, 1);
			yield return randomCharacter.CheckDeath();
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}
}
