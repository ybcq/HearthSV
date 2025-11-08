using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HeartStrike : SpellCard
{
	public HeartStrike()
	{
		this.Name = "心动打击";
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
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where m.IsDamaged()
		select m).ToList<Minion>();
		Minion firstMinion;
		Minion secondMinion;
		if (list.Count >= 2)
		{
			firstMinion = RNG.RandomItemFrom<Minion>(list);
			list.Remove(firstMinion);
			secondMinion = RNG.RandomItemFrom<Minion>(list);
		}
		else if (list.Count == 1)
		{
			firstMinion = list[0];
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
		InterfaceManager.Instance.SpawnDamageSplatOn(firstMinion.Controller, 2);
		yield return firstMinion.Damage(null, 2);
		InterfaceManager.Instance.SpawnDamageSplatOn(secondMinion.Controller, 2);
		yield return secondMinion.Damage(null, 2);
		yield return firstMinion.CheckDeath();
		yield return secondMinion.CheckDeath();
		yield break;
	}
}
