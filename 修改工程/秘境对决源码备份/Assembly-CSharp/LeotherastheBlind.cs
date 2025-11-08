using System;
using System.Collections;
using UnityEngine;

public class LeotherastheBlind : MinionCard
{
	public LeotherastheBlind()
	{
		this.Name = "黑暗精灵·芙蕾";
		this.Description = "When attacking, add 1 goblin card to your hand.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Elves;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.Mechanics.OnMinionAttacked.Add(new Func<MinionAttackedEvent, IEnumerator>(this.OnMinionAttacked));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionAttacked(MinionAttackedEvent evt)
	{
		if (evt.Minion == this.Minion)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			yield return this.Player.AddCardToHand(new HighWarlordNajentus());
		}
		yield break;
	}
}
