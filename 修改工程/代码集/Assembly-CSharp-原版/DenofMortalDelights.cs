using System;
using System.Collections;

public class DenofMortalDelights : SpellCard
{
	public DenofMortalDelights()
	{
		this.Name = "Den of Mortal Delights";
		this.Description = "Summon three Temple Concubines.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 7;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.SummonMinion(new TempleConcubine());
		yield return this.Player.SummonMinion(new TempleConcubine());
		yield return this.Player.SummonMinion(new TempleConcubine());
		yield break;
	}
}
