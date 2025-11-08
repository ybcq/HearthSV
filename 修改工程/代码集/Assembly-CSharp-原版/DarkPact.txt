using System;
using System.Collections;

public class DarkPact : BaseHeroPower
{
	public DarkPact(Hero hero)
	{
		this.Name = "Chi Burst";
		this.Description = "Give a friendly minion +1 attack; or give an enemy minion -1 Attack.";
		this.Class = HeroClass.DemonHunter;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 2;
		base.Initialize(hero);
	}

	public override IEnumerator Use(Character target)
	{
		if (target.IsFriendlyOf(this.Hero))
		{
			target.AddAttackModifier(new Func<int, int>(this.FriendlyAttackModifier));
		}
		else
		{
			target.AddAttackModifier(new Func<int, int>(this.EnemyAttackModifier));
		}
		yield break;
	}

	public int FriendlyAttackModifier(int attack)
	{
		return attack + 1;
	}

	public int EnemyAttackModifier(int attack)
	{
		return attack - 1;
	}

	public override IEnumerator Upgrade()
	{
		yield break;
	}

	public override bool CanUse()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Hero.Player).Count > 0;
	}
}
