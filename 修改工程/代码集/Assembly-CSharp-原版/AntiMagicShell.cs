using System;
using System.Collections;

public class AntiMagicShell : SpellCard
{
	public AntiMagicShell()
	{
		this.Name = "Anti-Magic Shell";
		this.Description = "Give a friendly minion Spellshield.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 0;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().HasSpellshield = true;
		yield break;
	}
}
