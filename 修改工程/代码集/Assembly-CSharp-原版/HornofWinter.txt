using System;
using System.Collections;

public class HornofWinter : SpellCard
{
	public HornofWinter()
	{
		this.Name = "Horn of Winter";
		this.Description = "Give your characters +1 Attack this turn.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Minion minion in this.Player.Minions)
		{
			Minion scopedMinion = minion;
			DisposableEvent<TurnEvent> disposable = null;
			minion.AddAttackModifier(new Func<int, int>(this.HornofWinterModifier));
			disposable = minion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, scopedMinion, disposable));
		}
		this.Player.Hero.AddAttackModifier(new Func<int, int>(this.HornofWinterModifier));
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add((TurnEvent x) => this.OnTurnEnd(x, this.Player.Hero, this.TurnEndSubscription));
		yield break;
	}

	public int HornofWinterModifier(int attack)
	{
		return attack + 1;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent, Character self, IDisposable disposable)
	{
		self.RemoveAttackModifier(new Func<int, int>(this.HornofWinterModifier));
		disposable.Dispose();
		yield break;
	}

	public IDisposable TurnEndSubscription;
}
