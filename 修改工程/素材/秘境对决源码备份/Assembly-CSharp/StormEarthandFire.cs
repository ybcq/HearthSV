using System;
using System.Collections;

public class StormEarthandFire : SpellCard
{
	public StormEarthandFire()
	{
		this.Name = "火土风暴";
		this.Description = "Summon 3 Elemental Pandarens. (2/2 Charge, 3/2 Windfury, 2/3 Taunt)";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 6;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count < 7;
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.SummonMinion(new FirePandaren());
		yield return this.Player.SummonMinion(new WindPandaren());
		yield return this.Player.SummonMinion(new EarthPandaren());
		yield break;
	}
}
