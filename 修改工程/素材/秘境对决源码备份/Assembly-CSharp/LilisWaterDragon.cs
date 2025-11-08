using System;
using System.Collections;
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
		return this.Player.Enemy.Minions.Count((Minion m) => m.Card.MinionType != MinionType.Totem) >= 2;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
		where m.IsAlive() && m.Card.MinionType != MinionType.Totem
		select m).ToList<Minion>());
		if (minion != null)
		{
			yield return minion.Destroy();
		}
		Minion minion2 = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
		where m.IsAlive() && m.Card.MinionType != MinionType.Totem
		select m).ToList<Minion>());
		if (minion2 != null)
		{
			minion2.Silence();
			minion2.Freeze();
		}
		yield break;
	}
}
