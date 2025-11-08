using System;
using System.Collections;

public class Shear : SpellCard
{
	public Shear()
	{
		this.Name = "Shear";
		this.Description = "Deal 2 damage, restore 3 Health to your hero, and summon a Soul Fragment.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Damage(null, 2 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield return this.Player.Hero.Heal(3);
		yield return this.Player.SummonMinion(new SoulFragment());
		yield break;
	}
}
