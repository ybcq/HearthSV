using System;
using System.Collections;
using UnityEngine;

public class InfernalStrike : SpellCard
{
	public InfernalStrike()
	{
		this.Name = "Infernal Strike";
		this.Description = "Set a minion's Attack to 0. Your hero gains as much Attack as was lost by that minion this turn.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		this.MinionAttack = minion.CurrentAttack;
		minion.AddAttackModifier(new Func<int, int>(this.InfernalStrikeMinionModifier));
		this.Player.Hero.AddAttackModifier(new Func<int, int>(this.InfernalStrikeHeroModifier));
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield break;
	}

	public int InfernalStrikeMinionModifier(int attack)
	{
		return 0;
	}

	public int InfernalStrikeHeroModifier(int attack)
	{
		return attack + this.MinionAttack;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		this.Player.Hero.RemoveAttackModifier(new Func<int, int>(this.InfernalStrikeHeroModifier));
		this.TurnEndSubscription.Dispose();
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public int MinionAttack;

	public IDisposable TurnEndSubscription;
}
