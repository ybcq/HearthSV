using System;
using System.Collections;

public class DarkCommand : SpellCard
{
	public DarkCommand()
	{
		this.Name = "Dark Command";
		this.Description = "Force an enemy minion to attack one of your minions.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.Collectible = false;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield break;
	}
}
