using System;
using System.Collections;
using System.Collections.Generic;

public class BroxigartheRed : MinionCard
{
	public BroxigartheRed()
	{
		this.Name = "布罗西加";
		this.Description = "Battlecry: Attack all enemies.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 10;
		this.BaseAttack = 4;
		this.BaseHealth = 16;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		List<Character> allEnemies = this.Player.Enemy.GetAllCharacters();
		int receivedDamage = 0;
		foreach (Character enemy in allEnemies)
		{
			receivedDamage += enemy.CurrentAttack;
			yield return enemy.Damage(this.Minion, base.CurrentAttack);
		}
		yield return this.Minion.Damage(null, receivedDamage);
		foreach (Character enemy2 in allEnemies)
		{
			yield return enemy2.CheckDeath();
		}
		yield return this.Minion.CheckDeath();
		yield break;
	}
}
