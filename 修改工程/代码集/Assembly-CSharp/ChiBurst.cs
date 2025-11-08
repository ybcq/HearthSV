using System;
using System.Collections;
using UnityEngine;

public class ChiBurst : BaseHeroPower
{
	public ChiBurst(Hero hero)
	{
		this.Name = "Chi Burst";
		this.Description = "+1 Attack this turn. Restore 1 Health.";
		this.Class = HeroClass.Monk;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 2;
		this.AttackModifier = new Func<int, int>(this.ApplyAttackModifier);
		base.Initialize(hero);
	}

	public override IEnumerator Use(Character target)
	{
		yield return target.Heal(1);
		this.Hero.AddAttackModifier(this.AttackModifier);
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		this.Hero.RemoveAttackModifier(this.AttackModifier);
		this.TurnEndSubscription.Dispose();
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 1;
	}

	public override IEnumerator Upgrade()
	{
		yield break;
	}

	public override bool CanUse()
	{
		return true;
	}

	public Func<int, int> AttackModifier;

	public IDisposable TurnEndSubscription;
}
