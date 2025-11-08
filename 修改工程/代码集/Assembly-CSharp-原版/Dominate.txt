using System;
using System.Collections;

public class Dominate : BaseHeroPower
{
	public Dominate(Hero hero)
	{
		this.Name = "Dominate";
		this.Description = "Take control of an enemy minion until end of turn.";
		this.Class = HeroClass.DeathKnight;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 2;
		base.Initialize(hero);
	}

	public override IEnumerator Use(Character target)
	{
		Minion targetMinion = (Minion)target;
		DisposableEvent<TurnEvent> disposable = null;
		yield return this.Hero.Player.TakeControlOf(targetMinion);
		disposable = targetMinion.Mechanics.OnTurnEnd.Add((TurnEvent evt) => this.OnTurnEnd(evt, targetMinion, disposable));
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

	public IEnumerator OnTurnEnd(TurnEvent turnEvent, Minion minion, DisposableEvent<TurnEvent> disposable)
	{
		yield return minion.Player.Enemy.TakeControlOf(minion);
		disposable.Dispose();
		yield break;
	}
}
