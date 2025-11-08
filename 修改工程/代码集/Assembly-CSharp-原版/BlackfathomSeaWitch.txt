using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class BlackfathomSeaWitch : MinionCard
{
	public BlackfathomSeaWitch()
	{
		this.Name = "Blackfathom Sea Witch";
		this.Description = "At the end of your turn, Freeze a random enemy character.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			Character randomCharacter = RNG.RandomItemFrom<Character>((from c in this.Player.Enemy.GetAllCharacters()
			where !c.IsFrozen
			select c).ToList<Character>());
			if (randomCharacter != null)
			{
				randomCharacter.Freeze();
			}
		}
		yield break;
	}
}
