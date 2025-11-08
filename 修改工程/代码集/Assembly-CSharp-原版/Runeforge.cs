using System;
using System.Collections;

public class Runeforge : SpellCard
{
	public Runeforge()
	{
		this.Name = "Runeforge";
		this.Description = "Discover a Rune.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.DiscoverCard(new RuneofLichbane(), new RuneofRazorice(), new RuneofSpellshattering());
		yield break;
	}
}
