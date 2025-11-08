using System;
using System.Collections;

public class Bloodworms : SpellCard
{
	public Bloodworms()
	{
		this.Name = "Bloodworms";
		this.Description = "Summon three 0/1 Bloodworms with \"Deathrattle: Restore 2 Health to your hero.\"";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count < 7;
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.SummonMinion(new Bloodworm());
		yield return this.Player.SummonMinion(new Bloodworm());
		yield return this.Player.SummonMinion(new Bloodworm());
		yield break;
	}
}
