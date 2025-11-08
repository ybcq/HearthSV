using System;
using System.Collections;
using UnityEngine;

public class Netherwalk : SpellCard
{
	public Netherwalk()
	{
		this.Name = "Netherwalk";
		this.Description = "Give a friendly minion Cannot Attack, Immune and Stealth until your next turn.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.CantAttack = true;
		targetMinion.IsStealth = true;
		targetMinion.IsImmune = true;
		this.TurnStartSubscription = targetMinion.Mechanics.OnTurnStart.Add((TurnEvent x) => this.OnTurnStart(x, targetMinion));
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent turnEvent, Minion self)
	{
		if (turnEvent.Player == this.Player)
		{
			self.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			self.CantAttack = false;
			self.IsStealth = false;
			self.IsImmune = false;
			this.TurnStartSubscription.Dispose();
		}
		yield break;
	}

	public IDisposable TurnStartSubscription;
}
