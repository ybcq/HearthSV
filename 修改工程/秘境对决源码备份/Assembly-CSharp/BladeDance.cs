using System;
using System.Collections;

public class BladeDance : SpellCard
{
	public BladeDance()
	{
		this.Name = "来自深渊的诱惑";
		this.Description = "Destroy an enemy's entourage. Necromancer 4; Summon 1 zombie to the battlefield.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.As<Minion>().Destroy();
		yield return target.CheckDeath();
		if (this.Player.DeadMinions.Count >= 4)
		{
			ExhumedLich minionCard = new ExhumedLich
			{
				BaseCost = 4,
				BaseAttack = 4,
				BaseHealth = 4,
				CurrentHealth = 4
			};
			yield return this.Player.SummonMinion(minionCard);
		}
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}
}
