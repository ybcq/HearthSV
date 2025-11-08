using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DunMoroghWendigo : MinionCard
{
	public DunMoroghWendigo()
	{
		this.Name = "冰原噬魂怪";
		this.Description = "Battlecry: Devour target creature.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.AllMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return GameManager.Instance.GetAllMinions().Any((Minion m) => m.Card.MinionType == MinionType.Biol);
	}

	public IEnumerator Battlecry(Character target)
	{
		this.targetMinion = (Minion)target;
		this.targetPlayer = target.Player;
		this.GainedAttack = this.targetMinion.CurrentAttack;
		this.GainedHealth = this.targetMinion.CurrentHealth;
		yield return this.targetMinion.Destroy();
		base.AddAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
		base.AddHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
		this.Minion.CurrentHealth += this.GainedHealth;
		yield break;
	}

	public IEnumerator Deathrattle(Minion self)
	{
		if (this.targetMinion != null)
		{
			yield return this.targetPlayer.SummonMinion(this.targetMinion.Card);
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + this.GainedAttack;
	}

	public int ApplyHealthModifier(int attack)
	{
		return attack + this.GainedHealth;
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && (target.IsFriendlyOf(this.Player.Hero) || !target.IsStealth) && target.IsMinion() && target.As<Minion>().Card.MinionType == MinionType.Biol;
	}

	public int GainedAttack;

	public int GainedHealth;

	public Minion targetMinion;

	public Player targetPlayer;
}
