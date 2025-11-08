using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EarthPandaren : MinionCard
{
	public EarthPandaren()
	{
		this.Name = "地熊猫";
		this.Description = "Taunt. Meditate: Discover a Common Card.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Murloc;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator Meditate(Player player)
	{
		List<BaseCard> cards = (from c in CardManager.Instance.AllCards
		where c.Rarity == CardRarity.Common
		select c).ToList<BaseCard>();
		this.Player.DiscoverCard(cards);
		yield break;
	}
}
