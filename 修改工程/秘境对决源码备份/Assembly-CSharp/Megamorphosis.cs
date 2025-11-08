using System;
using System.Collections;
using UnityEngine;

public class Megamorphosis : BaseHeroPower
{
	public Megamorphosis(Hero hero)
	{
		this.Name = "Megamorphosis";
		this.Description = "Give your Hero +3 Attack this turn and +3 Health.";
		this.Class = HeroClass.DemonHunter;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 2;
		this.AttackModifier = new Func<int, int>(this.ApplyAttackModifier);
		base.Initialize(hero);
	}

	public override IEnumerator Use(Character target)
	{
		yield return target.Heal(2);
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

	public override IEnumerator Upgrade()
	{
		yield break;
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 2;
	}

	public override bool CanUse()
	{
		return true;
	}

	public IDisposable TurnEndSubscription;

	public Func<int, int> AttackModifier;
}
