using System;
using System.Collections;
using System.Linq;

public class DrunkenBrewmaster : MinionCard
{
	public DrunkenBrewmaster()
	{
		this.Name = "醉酒的酒仙";
		this.Description = "Meditate: Discover a Spell Card of your Hero Class to your hand.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeMinion();
	}

	public IEnumerator Meditate(Player player)
	{
		SpellCard card = RNG.RandomItemFrom<SpellCard>((from m in CardManager.Instance.AllCards.OfType<SpellCard>()
		where m.Class == this.Player.Hero.Class
		select m).ToList<SpellCard>());
		yield return this.Player.AddCardToHand(card);
		yield break;
	}
}
