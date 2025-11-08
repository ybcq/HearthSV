using System;
using System.Collections;

public class VengefulSpirit : MinionCard
{
	public VengefulSpirit()
	{
		this.Name = "小鬼囚徒";
		this.Description = "Taunt, Deathrattle: Summon a 1/1 Ghoul.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.SummonMinion(new ChargeTurnGhoul
		{
			BaseAttack = 1,
			BaseHealth = 1,
			CurrentHealth = 1
		});
		yield break;
	}
}
