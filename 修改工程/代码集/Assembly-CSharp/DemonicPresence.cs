using System;
using System.Collections;
using System.Linq;

public class DemonicPresence : SpellCard
{
	public DemonicPresence()
	{
		this.Name = "龙之传令";
		this.Description = "Draw a card costs more than 5 and let it costs 1 less";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		MinionCard minionCard = RNG.RandomItemFrom<MinionCard>((from c in this.Player.Deck.OfType<MinionCard>()
		where c.BaseCost > 5
		select c).ToList<MinionCard>());
		if (minionCard != null)
		{
			minionCard.BaseCost--;
			yield return this.Player.DrawFromDeck(minionCard, null);
		}
		yield break;
	}
}
