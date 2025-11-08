using System;
using System.Collections;

public class GlaiveThrow : SpellCard
{
	public GlaiveThrow()
	{
		this.Name = "Glaive Throw";
		this.Description = "Deal 1 damage for each Attack you have.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Damage(null, this.Player.Hero.CurrentAttack + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield break;
	}
}
