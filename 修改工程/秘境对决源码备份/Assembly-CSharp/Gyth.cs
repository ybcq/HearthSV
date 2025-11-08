using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Gyth : MinionCard
{
	public Gyth()
	{
		this.Name = "脉轮守护者";
		this.Description = "Warcry: Transfer barriers, curtains, and hiding effects on target creatures to yourself.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.AllMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Minion.HasDivineShield = target.HasDivineShield;
		yield return this.Minion.HasSpellshield = target.HasSpellshield;
		yield return this.Minion.IsStealth = target.IsStealth;
		target.HasDivineShield = false;
		target.HasSpellshield = false;
		target.IsStealth = false;
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public override bool CanBattlecry()
	{
		return GameManager.Instance.GetAllMinions().Any((Minion m) => m.Card.MinionType == MinionType.Biol && (m.HasDivineShield || m.HasSpellshield || m.IsStealth));
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && (target.IsFriendlyOf(this.Player.Hero) || !target.IsStealth) && target.IsMinion() && target.As<Minion>().Card.MinionType == MinionType.Biol;
	}
}
