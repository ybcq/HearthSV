using System;
using System.Collections;

public class FrostStrike : SpellCard
{
	public FrostStrike()
	{
		this.Name = "Frost Strike";
		this.Description = "Deal 3 damage. This spell deals double damage to Frozen targets.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 3 + this.Player.GetSpellPower();
		if (target.IsFrozen)
		{
			damage *= 2;
		}
		yield return target.Damage(null, damage);
		yield return target.CheckDeath();
		yield break;
	}
}
