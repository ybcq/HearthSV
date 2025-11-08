using System;
using System.Collections;
using System.Linq;

public class BloodStrike : SpellCard
{
	public BloodStrike()
	{
		this.Name = "黑暗城堡";
		this.Description = "Discover an Knight which is Legendary.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		(from c in CardManager.Instance.AllCards.OfType<MinionCard>()
		where c.MinionType == MinionType.Dragon
		where c.Class == HeroClass.Neutral || c.Class == this.Player.Hero.Class
		select c).Cast<BaseCard>().ToList<BaseCard>();
		this.Player.DiscoverCard(new LordMarrowgar(), new TeronGorefiend(), new DarionMograine());
		yield break;
	}
}
