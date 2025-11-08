using System;
using System.Collections;

public class FlamingPumpkin : MinionCard
{
	public FlamingPumpkin()
	{
		this.Name = "阿彻鲁斯充能者";
		this.Description = "Battlecry: If a minion died this turn, gain Charge.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return GameManager.Instance.CurrentTurnDeadMinions > 0;
	}

	public IEnumerator Battlecry(Character target)
	{
		this.Minion.HasCharge = true;
		yield break;
	}
}
