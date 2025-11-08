using System;
using System.Collections;
using UnityEngine;

public class DunMoroghWendigo : MinionCard
{
	public DunMoroghWendigo()
	{
		this.Name = "Dun Morogh Wendigo ";
		this.Description = "Battlecry: Give a minion -1 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.AllMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return GameManager.Instance.GetAllMinions().Count > 0;
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return new WaitForSeconds(0.25f);
		target.As<Minion>().AddAttackModifier(new Func<int, int>(this.ApplyWendigoModifier));
		yield break;
	}

	public int ApplyWendigoModifier(int attack)
	{
		return attack - 1;
	}
}
