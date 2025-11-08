using System;
using System.Collections;

public class HiredThug : MinionCard
{
	public HiredThug()
	{
		this.Name = "Hired Thug";
		this.Description = "Deathrattle: Give your opponent a coin.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.Enemy.AddCardToHand(new Coin());
		yield break;
	}
}
