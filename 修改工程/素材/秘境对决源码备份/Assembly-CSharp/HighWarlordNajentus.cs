using System;
using System.Collections;

public class HighWarlordNajentus : MinionCard
{
	public HighWarlordNajentus()
	{
		this.Name = "影之诗融合怪";
		this.Description = "Empty.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		this.TurnStartSubscription = this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent turnEvent)
	{
		if (this.Player.IsCurrent())
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return this.Minion.Die();
		}
		yield break;
	}

	public IEnumerator Battlecry(Character target)
	{
		if (this.BaseCost != 0)
		{
			this.Minion.Mechanics.RemoveAll();
		}
		yield break;
	}

	public IDisposable TurnStartSubscription;
}
