using System;
using System.Collections;

public class Sporebat : MinionCard
{
	public Sporebat()
	{
		this.Name = "重生骨墙";
		this.Description = "嘲讽，亡语：召唤一个0/2嘲讽骨墙.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 1;
		this.BaseAttack = 0;
		this.BaseHealth = 2;
		this.HasTaunt = true;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		Sporebat sporebat = new Sporebat();
		yield return this.Player.SummonMinion(sporebat);
		if (sporebat.Minion != null)
		{
			sporebat.Minion.Mechanics.RemoveAll();
			sporebat.Minion.HasTaunt = true;
		}
		yield break;
	}
}
