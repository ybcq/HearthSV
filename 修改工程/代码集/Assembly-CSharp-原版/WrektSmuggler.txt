using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WrektSmuggler : MinionCard
{
	public WrektSmuggler()
	{
		this.Name = "Wrekt Smuggler";
		this.Description = "Battlecry: Discover a spell from another class.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		List<BaseCard> cards = (from c in CardManager.Instance.AllCards
		where c is SpellCard && c.Collectible && c.Class != this.Player.Hero.Class
		select c).ToList<BaseCard>();
		this.Player.DiscoverCard(cards);
		yield break;
	}
}
