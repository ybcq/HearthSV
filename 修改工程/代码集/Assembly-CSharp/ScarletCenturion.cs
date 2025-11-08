using System;
using System.Collections;

public class ScarletCenturion : MinionCard
{
	public ScarletCenturion()
	{
		this.Name = "猩红百夫长";
		this.Description = "Battlecry: Give your minions +1 Attack this turn.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.AttackModifier = new Func<int, int>(this.ScarletCenturionModifier);
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		this.Battlecry();
		yield break;
	}

	public void Battlecry()
	{
		foreach (Minion scopedMinion2 in this.Player.Minions)
		{
			Minion scopedMinion = scopedMinion2;
			DisposableEvent<TurnEvent> disposable = null;
			if (scopedMinion.IsAlive() && scopedMinion != this.Minion)
			{
				scopedMinion.AddAttackModifier(this.AttackModifier);
				disposable = scopedMinion.Mechanics.OnTurnEnd.Add((TurnEvent evt) => this.OnTurnEnd(evt, scopedMinion, disposable));
			}
		}
	}

	public int ScarletCenturionModifier(int attack)
	{
		return attack + 1;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent, Minion self, DisposableEvent<TurnEvent> disposable)
	{
		self.RemoveAttackModifier(this.AttackModifier);
		disposable.Dispose();
		yield break;
	}

	public Func<int, int> AttackModifier;

	public IDisposable TurnEndSubscription;
}
