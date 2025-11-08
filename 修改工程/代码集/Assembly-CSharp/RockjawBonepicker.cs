using System;
using System.Collections;

public class RockjawBonepicker : MinionCard
{
	public RockjawBonepicker()
	{
		this.Name = "颚骨";
		this.Description = "Battlecry: Deal 1 damage to a minion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
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
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 1);
		yield return target.Damage(null, 1);
		yield return target.CheckDeath();
		yield break;
	}
}
