using System;
using System.Collections;
using System.Collections.Generic;

public class Obliterate : SpellCard
{
	public Obliterate()
	{
		this.Name = "死亡之握";
		this.Description = "Steal a Card from your opponent deck.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<BaseCard> cardsOfType = this.Player.Enemy.Deck.GetCardsOfType<MinionCard>();
		if (cardsOfType.Count > 0)
		{
			BaseCard card = RNG.RandomItemFrom<BaseCard>(cardsOfType);
			yield return this.Player.AddCardToHand(card);
			this.Player.Enemy.RemoveCardFromDeck(card);
			card = null;
		}
		yield break;
	}
}
