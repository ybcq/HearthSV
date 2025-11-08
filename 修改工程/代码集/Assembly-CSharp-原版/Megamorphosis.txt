using System;
using System.Collections;

public class Megamorphosis : BaseHeroPower
{
	public Megamorphosis(Hero hero)
	{
		this.Name = "Megamorphosis";
		this.Description = "Give your Hero +3 Attack this turn and +3 Health.";
		this.Class = HeroClass.DemonHunter;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.Initialize(hero);
	}

	public override IEnumerator Use(Character target)
	{
		this.Hero.CurrentHealth += 3;
		this.Hero.AddHealthModifier(new Func<int, int>(this.MetamorphosisModifier));
		this.Hero.AddAttackModifier(new Func<int, int>(this.MetamorphosisModifier));
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		this.Hero.RemoveAttackModifier(new Func<int, int>(this.MetamorphosisModifier));
		this.TurnEndSubscription.Dispose();
		yield break;
	}

	public int MetamorphosisModifier(int value)
	{
		return value + 3;
	}

	public override IEnumerator Upgrade()
	{
		yield break;
	}

	public IDisposable TurnEndSubscription;
}
