using System;
using System.Collections;
using UnityEngine;

public class FlamingPumpkin : MinionCard
{
	public FlamingPumpkin()
	{
		this.Name = "生长型卡拉诺";
		this.Description = "Taunt. At the end of your turn, gain +1 life.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 1;
		this.BaseAttack = 0;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			base.AddHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
			this.Minion.CurrentHealth++;
		}
		yield break;
	}

	public int ApplyHealthModifier(int value)
	{
		return value + 1;
	}
}
