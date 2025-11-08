using System;
using System.Collections;

public class Obliterate : SpellCard
{
	public Obliterate()
	{
		this.Name = "Obliterate";
		this.Description = "Deal 8 damage.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 8;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Damage(null, 8);
		yield return target.CheckDeath();
		yield break;
	}
}
