using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HeartStrike : SpellCard
{
	public HeartStrike()
	{
		this.Name = "Heart Strike";
		this.Description = "Deal 2 damage to two random enemy minions. Prioritizes damaged minions.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.Count > 2;
	}

	public override IEnumerator Cast(Character target)
	{
		List<Minion> damagedMinions = (from m in this.Player.Enemy.Minions
		where m.IsDamaged()
		select m).ToList<Minion>();
		Minion firstMinion;
		Minion secondMinion;
		if (damagedMinions.Count >= 2)
		{
			firstMinion = RNG.RandomItemFrom<Minion>(damagedMinions);
			damagedMinions.Remove(firstMinion);
			secondMinion = RNG.RandomItemFrom<Minion>(damagedMinions);
		}
		else if (damagedMinions.Count == 1)
		{
			firstMinion = damagedMinions[0];
			secondMinion = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
			where !m.IsDamaged()
			select m).ToList<Minion>());
		}
		else
		{
			firstMinion = RNG.RandomItemFrom<Minion>(this.Player.Enemy.Minions);
			secondMinion = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
			where m != firstMinion
			select m).ToList<Minion>());
		}
		yield return firstMinion.Damage(null, 1);
		yield return secondMinion.Damage(null, 1);
		yield return firstMinion.CheckDeath();
		yield return secondMinion.CheckDeath();
		yield break;
	}
}
