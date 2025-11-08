using System;
using System.Collections;

public class SkullTap : SpellCard
{
	public SkullTap()
	{
		this.Name = "军师的妙计";
		this.Description = "Draw the same cards with your minions.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Draw(this.Player.Minions.Count, null);
		yield break;
	}
}
