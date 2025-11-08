using System;
using System.Collections;

public class EssenceofSuffering : SpellCard
{
	public EssenceofSuffering()
	{
		this.Name = "Essence of Suffering";
		this.Description = "Deal 2 damage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Damage(null, 2 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield break;
	}
}
