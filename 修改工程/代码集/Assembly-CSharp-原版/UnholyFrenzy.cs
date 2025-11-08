using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class UnholyFrenzy : SpellCard
{
	public UnholyFrenzy()
	{
		this.Name = "Unholy Frenzy";
		this.Description = "Give a minion +4 Attack. At the end of each turn, deal 1 damage to it.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = target.As<Minion>();
		this.UnholyFrenzyTarget = targetMinion;
		targetMinion.AddAttackModifier(new Func<int, int>(this.UnholyFrenzyModifier));
		targetMinion.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		yield return this.UnholyFrenzyTarget.Damage(null, 1);
		yield return this.UnholyFrenzyTarget.CheckDeath();
		yield break;
	}

	public int UnholyFrenzyModifier(int attack)
	{
		return attack + 4;
	}

	public Minion UnholyFrenzyTarget;
}
