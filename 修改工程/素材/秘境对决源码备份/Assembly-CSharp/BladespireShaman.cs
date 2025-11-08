using System;
using System.Collections;

public class BladespireShaman : MinionCard
{
	public BladespireShaman()
	{
		this.Name = "啃食僵尸";
		this.Description = "Necromancer 1: Gain +1 strength when entering the battlefield.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 1;
		this.BaseAttack = 2;
		this.BaseHealth = 1;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		if (this.Player.Enemy.DeadMinions.Count >= 1)
		{
			base.AddAttackModifier(new Func<int, int>(this.BladespireShamanModifier));
		}
		yield break;
	}

	public int BladespireShamanModifier(int value)
	{
		return value + 1;
	}
}
