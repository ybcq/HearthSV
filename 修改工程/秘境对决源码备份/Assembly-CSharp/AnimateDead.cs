using System;
using System.Collections;
using System.Linq;

public class AnimateDead : SpellCard
{
	public AnimateDead()
	{
		this.Name = "镜像亡者";
		this.Description = "Discover an enemy minion that died this game.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.DeadMinions.Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.DiscoverCard((from m in this.Player.Enemy.DeadMinions
		select m.Copy()).ToList<BaseCard>());
		yield break;
	}
}
