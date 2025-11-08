using System;
using System.Collections;

public class KoboldTunneler : MinionCard
{
	public KoboldTunneler()
	{
		this.Name = "Kobold Tunneler";
		this.Description = "Deathrattle: Give your opponent a Candle.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.Enemy.AddCardToHand(new Candle());
		yield break;
	}
}
