using System;
using System.Collections;
using System.Collections.Generic;

public class DeathandDecay : SpellCard
{
	public DeathandDecay()
	{
		this.Name = "死亡凋零";
		this.Description = "Deal 3 damage to all enemies.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 3 + this.Player.GetSpellPower();
		List<Character> aliveCharacters = this.Player.Enemy.GetAllCharacters();
		foreach (Character character in aliveCharacters)
		{
			if (character.IsMinion() && character.As<Minion>().Card.MinionType != MinionType.Totem)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(character.Controller, damage);
				yield return character.Damage(null, damage);
			}
			else if (character.IsHero())
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(character.Controller, damage);
				yield return character.Damage(null, damage);
			}
		}
		List<Character>.Enumerator enumerator = default(List<Character>.Enumerator);
		foreach (Character character2 in aliveCharacters)
		{
			yield return character2.CheckDeath();
		}
		enumerator = default(List<Character>.Enumerator);
		yield break;
		yield break;
	}
}
