using System;
using System.Collections;
using UnityEngine;

public class SeethingRage : SpellCard
{
	public SeethingRage()
	{
		this.Name = "Seething Rage";
		this.Description = "Freeze a minion. At the start of your next turn, double its Attack.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.Freeze();
		this.TurnStartSubscription = targetMinion.Mechanics.OnTurnStart.Add((TurnEvent x) => this.OnTurnStart(x, targetMinion));
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent evt, Minion self)
	{
		self.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		self.AddAttackModifier(new Func<int, int>(this.SeethingRageModifier));
		this.TurnStartSubscription.Dispose();
		yield break;
	}

	public int SeethingRageModifier(int attack)
	{
		return attack * 2;
	}

	public IDisposable TurnStartSubscription;
}
