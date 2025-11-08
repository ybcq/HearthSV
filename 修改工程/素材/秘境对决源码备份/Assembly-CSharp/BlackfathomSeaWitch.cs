using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class BlackfathomSeaWitch : MinionCard
{
	public BlackfathomSeaWitch()
	{
		this.Name = "萨菲隆";
		this.Description = "At the end of your turn, Freeze a random enemy character.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 6;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			Character character = RNG.RandomItemFrom<Character>((from c in this.Player.Enemy.GetAllCharacters()
			where !c.IsFrozen && c.IsMinion() && c.As<Minion>().Card.MinionType != MinionType.Totem
			select c).ToList<Character>());
			if (character != null)
			{
				character.Freeze();
			}
		}
		yield break;
	}
}
