using System;
using System.Collections;
using UnityEngine;

public class Torment : SpellCard
{
	public Torment()
	{
		this.Name = "Torment";
		this.Description = "Give a minion \"Can't attack minions with Taunt, and at the end of each turn, deal 1 damage to this minion.\"";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.CantAttackTaunt = true;
		targetMinion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, targetMinion));
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent, Minion self)
	{
		self.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		yield return self.Damage(null, 1);
		yield return self.CheckDeath();
		yield break;
	}
}
