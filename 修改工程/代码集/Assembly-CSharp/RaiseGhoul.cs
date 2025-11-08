using System;
using System.Collections;

public class RaiseGhoul : BaseHeroPower
{
	public RaiseGhoul(Hero hero)
	{
		this.Name = "Raise Ghoul";
		this.Description = "Summon a 1/1 Ghoul with Charge that dies at end of turn.";
		this.Class = HeroClass.DeathKnight;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.Initialize(hero);
	}

	public override IEnumerator Use(Character target)
	{
		yield return this.Hero.Player.SummonMinion(new ChargeTurnGhoul());
		yield break;
	}

	public override IEnumerator Upgrade()
	{
		yield break;
	}

	public override bool CanUse()
	{
		return this.Hero.Player.Minions.Count < 7;
	}
}
