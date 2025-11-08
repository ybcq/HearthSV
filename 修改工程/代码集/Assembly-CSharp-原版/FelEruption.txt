using System;
using System.Collections;

public class FelEruption : SpellCard
{
	public FelEruption()
	{
		this.Name = "Fel Eruption";
		this.Description = "Deal 4-5 damage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = RNG.RandomInteger(4, 5) + this.Player.GetSpellPower();
		yield return target.Damage(null, damage);
		yield return target.CheckDeath();
		yield break;
	}
}
