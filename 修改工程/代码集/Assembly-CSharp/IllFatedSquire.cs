using System;
using System.Collections;
using System.Collections.Generic;

public class IllFatedSquire : MinionCard
{
	public IllFatedSquire()
	{
		this.Name = "病娇乡绅";
		this.Description = "Deathrattle: Put a random weapon from your deck into your hand.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		if (self.Player.Deck.ContainsCardOfType<WeaponCard>())
		{
			List<BaseCard> cardsOfType = self.Player.Deck.GetCardsOfType<WeaponCard>();
			if (cardsOfType.Count > 0)
			{
				BaseCard card = RNG.RandomItemFrom<BaseCard>(cardsOfType);
				yield return self.Player.DrawFromDeck(card, null);
			}
		}
		yield break;
	}
}
