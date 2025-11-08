using System;
using System.Collections;
using System.Collections.Generic;

public class DeathandDecay : SpellCard
{
	public DeathandDecay()
	{
		this.Name = "Death and Decay";
		this.Description = "Deal 5 damage to all enemies.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 9;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 5 + this.Player.GetSpellPower();
		List<Character> aliveCharacters = this.Player.Enemy.GetAllCharacters();
		foreach (Character enemy in aliveCharacters)
		{
			yield return enemy.Damage(null, damage);
		}
		foreach (Character enemy2 in aliveCharacters)
		{
			yield return enemy2.CheckDeath();
		}
		yield break;
	}
}
