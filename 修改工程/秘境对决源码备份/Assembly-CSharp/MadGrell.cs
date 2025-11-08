using System;
using System.Collections;

public class MadGrell : MinionCard
{
	public MadGrell()
	{
		this.Name = "戴德拉守护灵";
		this.Description = "Whenever you lose life during your own turn, you get +1/+1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.IsStealth = true;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		this.Mechanics.OnHeroDamaged.Add(new Func<HeroDamagedEvent, IEnumerator>(this.OnHeroDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnHeroDamaged(HeroDamagedEvent evt)
	{
		if (evt.Hero == this.Player.Hero && this.turnPlayer == this.Player)
		{
			base.AddAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
			base.AddHealthModifier(new Func<int, int>(this.ApplyAttackModifier));
			this.Minion.CurrentHealth++;
		}
		yield break;
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 1;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		this.turnPlayer = evt.Player;
		yield break;
	}

	public Player turnPlayer;
}
