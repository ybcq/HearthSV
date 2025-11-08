using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BladeDance : SpellCard
{
	public BladeDance()
	{
		this.Name = "Blade Dance";
		this.Description = "Deal 1 damage to all enemy minions. Give your hero Evasion until your next turn.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<Minion> availableTargets = this.Player.Enemy.Minions.ToList<Minion>();
		foreach (Minion minion in availableTargets)
		{
			yield return minion.Damage(null, 1 + this.Player.GetSpellPower());
		}
		foreach (Minion minion2 in availableTargets)
		{
			yield return minion2.CheckDeath();
		}
		this.Player.Hero.IsEvasive = true;
		this.TurnStartSubscription = EventManager.Instance.TurnStartHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Player.Hero.SetEvasion(false);
			this.TurnStartSubscription.Dispose();
		}
		yield break;
	}

	public IDisposable TurnStartSubscription;
}
