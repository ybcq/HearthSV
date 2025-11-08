using System;
using System.Collections;
using System.Collections.Generic;

public class AntiMagicShell : SpellCard
{
	public AntiMagicShell()
	{
		this.Name = "反魔法护盾";
		this.Description = "Give all friendly minion Spellshield & +2/+2.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		using (List<Minion>.Enumerator enumerator = this.Player.Minions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				minion.AddAttackModifier(new Func<int, int>(this.AntiMagicModifier));
				minion.CurrentHealth += 2;
				minion.AddHealthModifier(new Func<int, int>(this.AntiMagicModifier));
				minion.HasSpellshield = true;
			}
			yield break;
		}
		yield break;
	}

	public int AntiMagicModifier(int value)
	{
		return value + 2;
	}
}
