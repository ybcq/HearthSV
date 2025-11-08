using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DemonBite : SpellCard
{
	public DemonBite()
	{
		this.Name = "Demon Bite";
		this.Description = "Give your hero +2 Attack this turn. If you have a Demon in your hand, give your hero +4 Attack instead.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.Player.Hand.OfType<MinionCard>().Any((MinionCard c) => c.MinionType == MinionType.Demon))
		{
			this.AttackModifier = new Func<int, int>(this.DemonBiteHigherModifier);
		}
		else
		{
			this.AttackModifier = new Func<int, int>(this.DemonBiteLowerModifier);
		}
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

	public int DemonBiteLowerModifier(int attack)
	{
		return attack + 2;
	}

	public int DemonBiteHigherModifier(int attack)
	{
		return attack + 4;
	}

	public Func<int, int> AttackModifier;

	public IDisposable TurnEndSubscription;
}
