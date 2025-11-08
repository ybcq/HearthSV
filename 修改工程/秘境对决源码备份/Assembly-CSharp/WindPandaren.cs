using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WindPandaren : MinionCard
{
	public WindPandaren()
	{
		this.Name = "风熊猫";
		this.Description = "Windfury. Meditate: Discover a Epic Card.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Murloc;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.HasWindfury = true;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator Meditate(Player player)
	{
		List<BaseCard> cards = (from c in CardManager.Instance.AllCards
		where c.Rarity == CardRarity.Epic
		select c).ToList<BaseCard>();
		this.Player.DiscoverCard(cards);
		yield break;
	}
}
