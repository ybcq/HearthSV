using System;
using System.Collections;
using UnityEngine;

public class Jab : SpellCard
{
	public Jab()
	{
		this.Name = "Jab";
		this.Description = "Give your hero +2 Attack this turn.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		this.AttackModifier = new Func<int, int>(this.ApplyAttackModifier);
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Hero.AddAttackModifier(this.AttackModifier);
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield return new WaitForSeconds(0.25f);
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
		return attack + 2;
	}

	public Func<int, int> AttackModifier;

	public IDisposable TurnEndSubscription;
}
