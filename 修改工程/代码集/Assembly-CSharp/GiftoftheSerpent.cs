using System;
using System.Collections;

public class GiftoftheSerpent : SpellCard
{
	public GiftoftheSerpent()
	{
		this.Name = "巨变者·周卓";
		this.Description = "Your Hero Power becomes \"Give your Hero +2 Attack this turn and +2 Health\". If already in Metamorphosis: +3/+3.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.BaseCost = 2;
		this.TargetType = TargetType.NoTarget;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.Player.Hero.HeroPower is MetamorphosisHP)
		{
			yield return this.Player.Hero.HeroPower.Upgrade();
		}
		else
		{
			yield return this.Player.ReplaceHeroPower(new MetamorphosisHP(this.Player.Hero));
		}
		yield break;
	}
}
