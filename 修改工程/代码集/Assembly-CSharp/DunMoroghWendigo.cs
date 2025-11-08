using System;
using System.Collections;
using UnityEngine;

public class DunMoroghWendigo : MinionCard
{
	public DunMoroghWendigo()
	{
		this.Name = "冰原噬魂怪";
		this.Description = "Battlecry: 吞噬目标生物。";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
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
		return GameManager.Instance.GetAllMinions().Count > 0;
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
		this.CurrentHealth += this.GainedHealth;
		yield return new WaitForSeconds(0.25f);
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

	public int GainedAttack;

	public int GainedHealth;

	public Minion targetMinion;

	public Player targetPlayer;
}
