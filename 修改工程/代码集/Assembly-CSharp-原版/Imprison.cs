using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Imprison : SpellCard
{
	public Imprison()
	{
		this.Name = "Imprison";
		this.Description = "Discover a Demon.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<BaseCard> cards = (from c in CardManager.Instance.AllCards.OfType<MinionCard>()
		where c.MinionType == MinionType.Demon
		where c.Class == HeroClass.Neutral || c.Class == this.Player.Hero.Class
		select c).Cast<BaseCard>().ToList<BaseCard>();
		this.Player.DiscoverCard(cards);
		yield break;
	}
}
