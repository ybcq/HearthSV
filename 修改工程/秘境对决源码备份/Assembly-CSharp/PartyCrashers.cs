using System;
using System.Collections;
using UnityEngine;

public class PartyCrashers : MinionCard
{
	public PartyCrashers()
	{
		this.Name = "女妖";
		this.Description = "Battlecry: Transform a minion into a 1/1 Ghoul.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 1;
		this.BattlecryType = BattlecryType.AllMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		ChargeTurnGhoul chargeTurnGhoul = new ChargeTurnGhoul();
		target.As<Minion>().TransformInto(chargeTurnGhoul);
		if (chargeTurnGhoul.Minion != null)
		{
			chargeTurnGhoul.Minion.HasCharge = false;
			chargeTurnGhoul.Minion.Mechanics.RemoveAll();
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && (target.IsFriendlyOf(this.Player.Hero) || !target.IsStealth) && target.IsMinion() && target.As<Minion>().Card.MinionType != MinionType.Totem;
	}
}
