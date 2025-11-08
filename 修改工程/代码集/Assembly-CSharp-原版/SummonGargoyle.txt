using System;
using System.Collections;

public class SummonGargoyle : SpellCard
{
	public SummonGargoyle()
	{
		this.Name = "Summon Gargoyle";
		this.Description = "Choose an enemy character. Summon a 2/4 Gargoyle that deals 1 damage to it at the end of each turn.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.EnemyCharacters;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Gargoyle gargoyle = new Gargoyle
		{
			Target = target
		};
		yield return this.Player.SummonMinion(gargoyle);
		yield break;
	}
}
