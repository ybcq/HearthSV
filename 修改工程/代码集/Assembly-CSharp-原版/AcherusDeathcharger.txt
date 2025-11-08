using System;
using System.Collections;

public class AcherusDeathcharger : MinionCard
{
	public AcherusDeathcharger()
	{
		this.Name = "Acherus Deathcharger";
		this.Description = "Battlecry: If a minion died this turn, gain Charge.";
		this.Class = HeroClass.DeathKnight;
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
