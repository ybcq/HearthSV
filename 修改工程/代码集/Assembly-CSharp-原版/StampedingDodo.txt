using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StampedingDodo : MinionCard
{
	public StampedingDodo()
	{
		this.Name = "Stampeding Dodo";
		this.Description = "Battlecry: Destroy a random enemy minion with 1 or less Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		List<Minion> enemyTargeteableMinions = (from m in this.Player.Enemy.Minions
		where m.CurrentAttack <= 1
		select m).ToList<Minion>();
		if (enemyTargeteableMinions.Count > 0)
		{
			Minion targetMinion = RNG.RandomItemFrom<Minion>(enemyTargeteableMinions);
			yield return targetMinion.Destroy();
		}
		yield break;
	}
}
