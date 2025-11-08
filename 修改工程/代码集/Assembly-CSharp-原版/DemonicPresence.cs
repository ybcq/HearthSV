using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DemonicPresence : SpellCard
{
	public DemonicPresence()
	{
		this.Name = "Demonic Presence";
		this.Description = "Draw a Demon. If you're holding at least 6 Demons, summon them all from your hand.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<MinionCard> demons = (from c in this.Player.Deck.OfType<MinionCard>()
		where c.MinionType == MinionType.Demon
		select c).ToList<MinionCard>();
		MinionCard demonCard = RNG.RandomItemFrom<MinionCard>(demons);
		if (demonCard != null)
		{
			yield return this.Player.DrawFromDeck(demonCard, null);
		}
		if (this.Player.Hand.OfType<MinionCard>().Count((MinionCard c) => c.MinionType == MinionType.Demon) >= 6)
		{
			foreach (MinionCard demon in from c in this.Player.Hand.OfType<MinionCard>()
			where c.MinionType == MinionType.Demon
			select c)
			{
				yield return this.Player.SummonMinion(demon);
				this.Player.RemoveCardFromHand(demon);
			}
		}
		yield break;
	}
}
