using System;
using System.Collections;

public class ChargeTurnGhoul : MinionCard
{
	public ChargeTurnGhoul()
	{
		this.Name = "食尸鬼";
		this.Description = "Charge. At the end of your turn destroy this minion.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.HasCharge = true;
		this.TurnEndSubscription = this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		if (this.Player.IsCurrent())
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return this.Minion.Die();
		}
		yield break;
	}

	public IDisposable TurnEndSubscription;
}
