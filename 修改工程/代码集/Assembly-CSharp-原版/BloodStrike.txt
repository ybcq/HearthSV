using System;
using System.Collections;

public class BloodStrike : SpellCard
{
	public BloodStrike()
	{
		this.Name = "Blood Strike";
		this.Description = "Deal 4 damage. Restore 4 Health to your hero.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 4 + this.Player.GetSpellPower();
		yield return target.Damage(null, damage);
		yield return target.CheckDeath();
		yield return this.Player.Hero.Heal(4);
		yield break;
	}
}
