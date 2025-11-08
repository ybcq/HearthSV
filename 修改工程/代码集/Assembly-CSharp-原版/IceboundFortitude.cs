using System;
using System.Collections;

public class IceboundFortitude : SpellCard
{
	public IceboundFortitude()
	{
		this.Name = "Icebound Fortitude";
		this.Description = "Gain 9 armor. Freeze your hero.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Hero.CurrentArmor += 9;
		this.Player.Hero.Freeze();
		yield break;
	}
}
