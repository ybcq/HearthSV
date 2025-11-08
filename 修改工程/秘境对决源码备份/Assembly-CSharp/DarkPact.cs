using System;
using System.Collections;
using System.Linq;

public class DarkPact : BaseHeroPower
{
	public DarkPact(Hero hero)
	{
		this.Name = "进化";
		this.Description = "Give a friendly minion +2/+2 attack and Charge.";
		this.Class = HeroClass.DemonHunter;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 0;
		this.UseCount = 3;
		base.Initialize(hero);
	}

	public override IEnumerator Use(Character target)
	{
		target.AddAttackModifier(new Func<int, int>(this.FriendlyAttackModifier));
		target.AddHealthModifier(new Func<int, int>(this.FriendlyHealthModifier));
		target.CurrentHealth += 2;
		if (!target.HasCharge && target.IsSleeping)
		{
			target.CantAttackHeroes = true;
		}
		target.HasCharge = true;
		target.HasEvolution = true;
		this.UseCount--;
		Minion targetMinion = (Minion)target;
		targetMinion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, targetMinion));
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
		return this.Hero.Player.Minions.TargeteablesBySpellOf(this.Hero.Player).Any<Minion>() && this.Hero.Player.TurnMana >= 3 && this.UseCount > 0;
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

	public override bool CanTarget(Character target)
	{
		return target != null && target.IsFriendlyOf(this.Hero) && !target.HasSpellshield && !target.HasEvolution && target.IsMinion();
	}

	public int UseCount;
}
