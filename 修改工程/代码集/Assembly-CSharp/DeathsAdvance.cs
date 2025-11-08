using System;
using System.Collections;
using System.Linq;

public class DeathsAdvance : SpellCard
{
	public DeathsAdvance()
	{
		this.Name = "埋骨之地";
		this.Description = "Discover a Knight, a Dragon or a Beast, all of which cost 4.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		(from c in CardManager.Instance.AllCards.OfType<MinionCard>()
		where c.MinionType == MinionType.Beast
		where c.Class == HeroClass.Neutral || c.Class == this.Player.Hero.Class
		select c).Cast<BaseCard>().ToList<BaseCard>();
		this.Player.DiscoverCard(new DarkRiderofAcherus(), new HungryWyrmling(), new Invincible());
		yield break;
	}
}
