using System;
using System.Collections;

public class HungryWyrmling : MinionCard
{
	public HungryWyrmling()
	{
		this.Name = "饥饿之龙";
		this.Description = "Deathrattle: Summon a 2/2 Frozen Ghoul.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		ChargeTurnGhoul HungryWyrmlingCard = new ChargeTurnGhoul
		{
			BaseCost = 2,
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		};
		yield return self.Player.SummonMinion(HungryWyrmlingCard);
		if (HungryWyrmlingCard.Minion != null)
		{
			HungryWyrmlingCard.Minion.Mechanics.RemoveAll();
			HungryWyrmlingCard.Minion.Freeze();
		}
		yield break;
	}
}
