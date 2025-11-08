using System;
using System.Collections;

public class DarkPact : BaseHeroPower
{
	public DarkPact(Hero hero)
	{
		this.Name = "进化";
		this.Description = "Give a friendly minion +2/+2 attack and Charge.";
		this.Class = HeroClass.DemonHunter;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 0;
		base.Initialize(hero);
	}

	public override IEnumerator Use(Character target)
	{
		if (!target.HasEvolution)
		{
			target.AddAttackModifier(new Func<int, int>(this.FriendlyAttackModifier));
			target.AddHealthModifier(new Func<int, int>(this.FriendlyHealthModifier));
			target.CurrentHealth += 2;
			target.HasCharge = true;
			target.CantAttackHeroes = true;
			target.HasEvolution = true;
			Minion targetMinion = (Minion)target;
			targetMinion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, targetMinion));
		}
		else
		{
			this.CurrentUses = 1;
		}
		yield break;
	}

	public int FriendlyAttackModifier(int attack)
	{
		return attack + 2;
	}

	public override IEnumerator Upgrade()
	{
		yield break;
	}

	public override bool CanUse()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Hero.Player).Count > 0 && this.Hero.Player.TurnMana >= 3;
	}

	public int FriendlyHealthModifier(int health)
	{
		return health + 2;
	}

	public IEnumerator OnTurnEnd(TurnEvent evt, Minion self)
	{
		self.CantAttackHeroes = false;
		yield break;
	}
}
