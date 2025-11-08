using System;
using System.Collections;
using System.Collections.Generic;

public class GreyheartNetherMage : MinionCard
{
	public GreyheartNetherMage()
	{
		this.Name = "怨念的魔女";
		this.Description = "Battlecry: Deal 1 damage to all enemies.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		List<Character> enemies = this.Player.Enemy.GetAllCharacters();
		foreach (Character character in enemies)
		{
			yield return character.Damage(null, 1);
		}
		List<Character>.Enumerator enumerator = default(List<Character>.Enumerator);
		foreach (Character character2 in enemies)
		{
			yield return character2.CheckDeath();
		}
		enumerator = default(List<Character>.Enumerator);
		yield break;
		yield break;
	}
}
