using System;
using System.Collections;
using System.Collections.Generic;

public class AcherusDeathcharger : MinionCard
{
	public AcherusDeathcharger()
	{
		this.Name = "吸引仇恨的憎恶";
		this.Description = "Battlecry: Attack all enemies.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Undead;
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
		foreach (Character character in allEnemies)
		{
			receivedDamage += character.CurrentAttack;
			if (character.IsMinion() && character.As<Minion>().Card.MinionType != MinionType.Totem)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(character.Controller, base.CurrentAttack);
				yield return character.Damage(this.Minion, base.CurrentAttack);
			}
			else if (character.IsHero())
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(character.Controller, base.CurrentAttack);
				yield return character.Damage(this.Minion, base.CurrentAttack);
			}
		}
		List<Character>.Enumerator enumerator = default(List<Character>.Enumerator);
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Minion.Controller, receivedDamage);
		yield return this.Minion.Damage(null, receivedDamage);
		foreach (Character character2 in allEnemies)
		{
			yield return character2.CheckDeath();
		}
		enumerator = default(List<Character>.Enumerator);
		yield return this.Minion.CheckDeath();
		yield break;
		yield break;
	}
}
