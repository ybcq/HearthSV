using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RiverpawOutrunner : MinionCard
{
	public RiverpawOutrunner()
	{
		this.Name = "河爪局外人";
		this.Description = "Battlecry: Discover a Legendary minion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		List<BaseCard> cards = (from c in CardManager.Instance.AllCards
		where c is MinionCard && c.As<MinionCard>().Rarity == CardRarity.Legendary && (c.Class == this.Player.Hero.Class || c.Class == HeroClass.Neutral)
		select c).ToList<BaseCard>();
		this.Player.DiscoverCard(cards);
		yield break;
	}
}
