using System;
using System.Collections;

public class HungryWyrmling : MinionCard
{
	public HungryWyrmling()
	{
		this.Name = "Hungry Wyrmling";
		this.Description = "Deathrattle: Summon a 2/2 Frozen Ghoul.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		FrozenGhoul frozenGhoul = new FrozenGhoul();
		yield return self.Player.SummonMinion(frozenGhoul);
		if (frozenGhoul.Minion != null)
		{
			frozenGhoul.Minion.Freeze();
		}
		yield break;
	}
}
