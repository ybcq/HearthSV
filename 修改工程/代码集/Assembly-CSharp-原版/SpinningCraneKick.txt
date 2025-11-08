using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpinningCraneKick : SpellCard
{
	public SpinningCraneKick()
	{
		this.Name = "Spinning Crane Kick";
		this.Description = "Deal 2 damage to all enemy minions. Give your hero +3 Attack this turn.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 5;
		this.AttackModifier = new Func<int, int>(this.ApplyAttackModifier);
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 2 + this.Player.GetSpellPower();
		List<Minion> aliveMinions = (from m in this.Player.Enemy.Minions
		where m.IsAlive()
		select m).ToList<Minion>();
		foreach (Minion minion in aliveMinions)
		{
			yield return minion.Damage(null, damage);
		}
		foreach (Minion minion2 in aliveMinions)
		{
			yield return minion2.CheckDeath();
		}
		this.Player.Hero.AddAttackModifier(this.AttackModifier);
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		this.Player.Hero.RemoveAttackModifier(this.AttackModifier);
		this.TurnEndSubscription.Dispose();
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 3;
	}

	public Func<int, int> AttackModifier;

	public IDisposable TurnEndSubscription;
}
