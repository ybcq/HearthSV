using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class FireNovaTotem : MinionCard
{
	public FireNovaTotem()
	{
		this.Name = "阿扎达斯";
		this.Description = "At the end of your turn, turn a random enemy minion into a 0/2 Statue.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player && RNG.RandomItemFrom<Minion>((from m in GameManager.Instance.GetAllMinions()
		where m.Card.MinionType != MinionType.Totem && m.IsFriendlyOf(this.Player.Enemy.Hero)
		select m).ToList<Minion>()) != null)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			Sporebat sporebat = new Sporebat();
			this.Player.SummonMinion(sporebat);
			if (sporebat.Minion != null)
			{
				sporebat.Minion.Mechanics.RemoveAll();
			}
		}
		yield break;
	}
}
