using System;
using System.Collections;

public class HungryWyrmling : MinionCard
{
	public HungryWyrmling()
	{
		this.Name = "饥饿之龙";
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
		ChargeTurnGhoul frozenGhoul = new ChargeTurnGhoul
		{
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		};
		yield return self.Player.SummonMinion(frozenGhoul);
		if (frozenGhoul.Minion != null)
		{
			frozenGhoul.Minion.Silence();
			frozenGhoul.Minion.Freeze();
		}
		yield break;
	}
}
