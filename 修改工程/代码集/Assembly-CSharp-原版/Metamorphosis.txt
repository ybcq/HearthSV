using System;
using System.Collections;

public class Metamorphosis : SpellCard
{
	public Metamorphosis()
	{
		this.Name = "Metamorphosis";
		this.Description = "Your Hero Power becomes \"Give your Hero +2 Attack this turn and +2 Health\". If already in Metamorphosis: +3/+3.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
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
