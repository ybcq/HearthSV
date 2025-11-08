using System;
using System.Collections;

public class BloodTap : SpellCard
{
	public BloodTap()
	{
		this.Name = "Blood Tap";
		this.Description = "Take 2 damage. Draw a card.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Hero.Damage(null, 2);
		yield return this.Player.Hero.CheckDeath();
		yield return this.Player.Draw(null);
		yield break;
	}
}
