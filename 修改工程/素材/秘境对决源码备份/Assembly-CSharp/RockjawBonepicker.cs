using System;
using System.Collections;
using System.Collections.Generic;

public class RockjawBonepicker : MinionCard
{
	public RockjawBonepicker()
	{
		this.Name = "末日机器人";
		this.Description = "Battlecry and Deathrattle: Deal 5 damage to ALL minions.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Mech;
		this.BaseCost = 5;
		this.BaseAttack = 1;
		this.BaseHealth = 7;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		int damage = 5;
		List<Character> aliveCharacters = GameManager.Instance.GetAllCharacters();
		foreach (Character character in aliveCharacters)
		{
			if (character.IsMinion() && character != this.Minion && character.As<Minion>().Card.MinionType != MinionType.Totem)
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

	public IEnumerator Deathrattle(Minion target)
	{
		int damage = 5;
		List<Character> aliveCharacters = this.Player.Enemy.GetAllCharacters();
		foreach (Character character in aliveCharacters)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(character.Controller, damage);
			yield return character.Damage(null, damage);
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
