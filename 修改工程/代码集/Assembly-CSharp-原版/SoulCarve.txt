using System;
using System.Collections;
using UnityEngine;

public class SoulCarve : SpellCard
{
	public SoulCarve()
	{
		this.Name = "Soul Carve";
		this.Description = "Choose a minion. At the end of each turn, deal 1 damage to it and summon a Soul Fragment.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, targetMinion));
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent evt, Minion self)
	{
		self.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		yield return self.Damage(null, 1);
		yield return self.CheckDeath();
		yield return this.Player.SummonMinion(new SoulFragment());
		yield break;
	}
}
