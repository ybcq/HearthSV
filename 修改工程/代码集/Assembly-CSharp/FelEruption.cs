using System;
using System.Collections;

public class FelEruption : SpellCard
{
	public FelEruption()
	{
		this.Name = "恶魔冲击波";
		this.Description = "Deal 3 damage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyCharacters;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 3 + this.Player.GetSpellPower();
		yield return target.Damage(null, damage);
		yield return target.CheckDeath();
		yield break;
	}
}
