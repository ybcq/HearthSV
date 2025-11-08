using System;
using System.Collections;
using UnityEngine;

public class HighWarlordNajentus : MinionCard
{
	public HighWarlordNajentus()
	{
		this.Name = "High Warlord Naj'entus";
		this.Description = "Immune. Taunt. At the end of your turn, give your opponent an Impaling Spine.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 9;
		this.BaseAttack = 9;
		this.BaseHealth = 9;
		this.IsImmune = true;
		this.HasTaunt = true;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		if (this.Player.IsCurrent())
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			yield return this.Player.Enemy.AddCardToHand(new ImpalingSpine());
		}
		yield break;
	}
}
