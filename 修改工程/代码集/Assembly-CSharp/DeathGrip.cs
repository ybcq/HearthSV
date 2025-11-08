using System;
using System.Collections;
using System.Linq;

public class DeathGrip : SpellCard
{
	public DeathGrip()
	{
		this.Name = "凋零埋葬";
		this.Description = "Choose an enemy minion. Put it into your hand.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 7;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.As<Minion>().ReturnToEnemyHand();
		yield break;
	}
}
