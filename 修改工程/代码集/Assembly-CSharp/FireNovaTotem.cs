using System;
using System.Collections;

public class FireNovaTotem : MinionCard
{
	public FireNovaTotem()
	{
		this.Name = "枯萎的树人";
		this.Description = "At the start of your turn, get -1/-1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Mech;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 8;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Minion.AddAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
			this.Minion.AddHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
			this.Minion.CurrentHealth--;
		}
		yield break;
	}

	public int ApplyAttackModifier(int value)
	{
		return value - 1;
	}

	public int ApplyHealthModifier(int value)
	{
		return value - 1;
	}
}
