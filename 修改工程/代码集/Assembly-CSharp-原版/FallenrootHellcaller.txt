using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class FallenrootHellcaller : MinionCard
{
	public FallenrootHellcaller()
	{
		this.Name = "Fallenroot Hellcaller";
		this.Description = "At the end of your turn, deal 2 damage to a non-Demon.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 3;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			Character randomCharacter = RNG.RandomItemFrom<Minion>((from m in GameManager.Instance.GetAllMinions()
			where m.Card.MinionType != MinionType.Demon
			select m).ToList<Minion>());
			if (randomCharacter != null)
			{
				this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
				yield return new WaitForSeconds(0.5f);
				yield return randomCharacter.Damage(null, 2);
				yield return randomCharacter.CheckDeath();
			}
		}
		yield break;
	}
}
