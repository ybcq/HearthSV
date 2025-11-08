using System;
using System.Collections;

public class ManaTea : SpellCard
{
	public ManaTea()
	{
		this.Name = "Mana Tea";
		this.Description = "Reduce the cost of spells in your hand by (2).";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (BaseCard baseCard in this.Player.Hand)
		{
			if (baseCard is SpellCard)
			{
				baseCard.AddCostModifier(new Func<int, int>(this.ManaTeaModifier));
			}
		}
		yield break;
	}

	public int ManaTeaModifier(int mana)
	{
		return mana - 2;
	}
}
