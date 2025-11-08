using System;
using System.Collections;

public class RaiseDead : SpellCard
{
	public RaiseDead()
	{
		this.Name = "嗜血食尸鬼";
		this.Description = "Summon a 3/3 Ghoul. Costs (1) less for each minion that died this turn.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.AddCostModifier(new Func<int, int>(this.MinionDiedModifier));
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count < 7;
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.SummonMinion(new ChargeTurnGhoul());
		yield break;
	}

	public int MinionDiedModifier(int cost)
	{
		return cost - GameManager.Instance.CurrentTurnDeadMinions;
	}
}
