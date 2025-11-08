using System;
using System.Collections;
using System.Collections.Generic;

public class GreyheartNetherMage : MinionCard
{
	public GreyheartNetherMage()
	{
		this.Name = "Greyheart Nether-Mage";
		this.Description = "Battlecry: Deal 2 damage to all enemies, destroy a random enemy minion, or Freeze all enemies (chosen randomly).";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 7;
		this.BaseAttack = 6;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		int randomBattlecry = RNG.RandomInteger(0, 2);
		if (randomBattlecry != 0)
		{
			if (randomBattlecry != 1)
			{
				if (randomBattlecry == 2)
				{
					foreach (Character character in this.Player.Enemy.GetAllCharacters())
					{
						character.Freeze();
					}
				}
			}
			else
			{
				Minion randomMinion = RNG.RandomItemFrom<Minion>(this.Player.Enemy.Minions);
				if (randomMinion != null)
				{
					yield return randomMinion.Destroy();
				}
			}
		}
		else
		{
			List<Character> enemies = this.Player.Enemy.GetAllCharacters();
			foreach (Character enemy in enemies)
			{
				yield return enemy.Damage(null, 2);
			}
			foreach (Character enemy2 in enemies)
			{
				yield return enemy2.CheckDeath();
			}
		}
		yield break;
	}
}
