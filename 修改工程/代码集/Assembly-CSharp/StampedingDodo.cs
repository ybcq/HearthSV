using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StampedingDodo : MinionCard
{
	public StampedingDodo()
	{
		this.Name = "帕奇维克";
		this.Description = "Battlecry: Destroy a random enemy minion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 8;
		this.BaseAttack = 5;
		this.BaseHealth = 8;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where m.CurrentAttack >= 0
		select m).ToList<Minion>();
		if (list.Count > 0)
		{
			Minion minion = RNG.RandomItemFrom<Minion>(list);
			yield return minion.Destroy();
		}
		yield break;
	}
}
