using System;
using System.Collections;

public class GlaiveThrow : SpellCard
{
	public GlaiveThrow()
	{
		this.Name = "白银的箭击";
		this.Description = "Deal 1 damage for each Card you have.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.EnemyCharacters;
		this.BaseCost = 9;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Damage(null, this.Player.Hand.Count + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield break;
	}
}
