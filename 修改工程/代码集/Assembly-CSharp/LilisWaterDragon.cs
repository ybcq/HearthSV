using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LilisWaterDragon : SpellCard
{
	public LilisWaterDragon()
	{
		this.Name = "丽丽的水龙";
		this.Description = "Destroy a random enemy minion. Silence and Freeze another random enemy minion.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.Count >= 2;
	}

	public override IEnumerator Cast(Character target)
	{
		List<Minion> list = this.Player.Enemy.Minions.ToList<Minion>();
		Minion minion = RNG.RandomItemFrom<Minion>(list);
		list.Remove(minion);
		Minion minion2 = RNG.RandomItemFrom<Minion>(this.Player.Enemy.Minions.ToList<Minion>());
		minion2.Silence();
		minion2.Freeze();
		yield return minion.Destroy();
		yield break;
	}
}
