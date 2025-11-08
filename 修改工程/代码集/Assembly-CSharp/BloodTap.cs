using System;
using System.Collections;
using System.Linq;

public class BloodTap : SpellCard
{
	public BloodTap()
	{
		this.Name = "亡灵大厅";
		this.Description = "Discover an undead which costs 3.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		(from c in CardManager.Instance.AllCards.OfType<MinionCard>()
		where c.MinionType == MinionType.Undead
		where c.Class == HeroClass.Neutral || c.Class == this.Player.Hero.Class
		select c).Cast<BaseCard>().ToList<BaseCard>();
		this.Player.DiscoverCard(new HowlingBanshee(), new NerubianSycophant(), new ScourgeNecromancer());
		yield break;
	}
}
