using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnowLeopard : MinionCard
{
	public SnowLeopard()
	{
		this.Name = "炽燃雕像";
		this.Description = "At the end of your turn, deal 2 damage to all enemies.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Murloc;
		this.BaseCost = 8;
		this.BaseAttack = 0;
		this.BaseHealth = 9;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			List<Character> aliveMinions = (from m in this.Player.Enemy.GetAllCharacters()
			where m.IsAlive()
			select m).ToList<Character>();
			foreach (Character character in aliveMinions)
			{
				if (character.IsMinion() && character.As<Minion>().Card.MinionType != MinionType.Totem)
				{
					InterfaceManager.Instance.SpawnDamageSplatOn(character.Controller, 2);
					yield return character.Damage(null, 2);
				}
				else if (character.IsHero())
				{
					InterfaceManager.Instance.SpawnDamageSplatOn(character.Controller, 2);
					yield return character.Damage(null, 2);
				}
			}
			List<Character>.Enumerator enumerator = default(List<Character>.Enumerator);
			foreach (Character character2 in aliveMinions)
			{
				yield return character2.CheckDeath();
			}
			enumerator = default(List<Character>.Enumerator);
			yield break;
		}
		yield break;
		yield break;
	}
}
