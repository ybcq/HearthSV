using System;
using System.Collections;
using System.Linq;

public class AntiMagicZone : SpellCard
{
	public AntiMagicZone()
	{
		this.Name = "Anti-Magic Zone";
		this.Description = "Increase the Cost of spells in your opponent's hand by (2).";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (SpellCard spellCard in this.Player.Enemy.Hand.OfType<SpellCard>())
		{
			spellCard.AddCostModifier(new Func<int, int>(this.AntiMagicZoneModifier));
		}
		yield break;
	}

	public int AntiMagicZoneModifier(int cost)
	{
		return cost + 2;
	}
}
