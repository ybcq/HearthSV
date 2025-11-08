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
		this.MinionType = MinionType.Wizard;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		List<Minion> enemies = this.Player.Enemy.Minions;
		foreach (Minion minion in enemies)
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, 1);
				yield return minion.Damage(null, 1);
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		foreach (Character character in enemies)
		{
			yield return character.CheckDeath();
		}
		enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
