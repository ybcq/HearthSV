using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class SuddenDoom : SpellCard
{
	public SuddenDoom()
	{
		this.Name = "Sudden Doom";
		this.Description = "Choose a minion. Whenever it attacks, reduce the cost of spells in your hand by (1).";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.Mechanics.OnAttacked.Add((AttackedEvent x) => this.OnAttacked(x, targetMinion));
		yield break;
	}

	public IEnumerator OnAttacked(AttackedEvent evt, Minion target)
	{
		target.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		foreach (SpellCard spellCard in this.Player.Hand.OfType<SpellCard>())
		{
			spellCard.AddCostModifier(new Func<int, int>(this.SuddenDoomModifier));
		}
		yield break;
	}

	public int SuddenDoomModifier(int cost)
	{
		return cost - 1;
	}
}
