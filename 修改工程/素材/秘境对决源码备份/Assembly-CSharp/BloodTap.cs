using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BloodTap : SpellCard
{
	public BloodTap()
	{
		this.Name = "血丝";
		this.Description = "Destroy your Ghouls. Restore 1 Health and Get an Armor for each.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int destroyedFragments = 1;
		foreach (Minion minion in (from m in this.Player.Minions
		where m.Card is ChargeTurnGhoul
		select m).ToList<Minion>())
		{
			int num = destroyedFragments;
			destroyedFragments = num + 1;
			yield return minion.Destroy();
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		this.Player.Hero.CurrentArmor = this.Player.Hero.CurrentArmor + destroyedFragments;
		yield return target.Heal(destroyedFragments);
		yield break;
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Any((Minion m) => m.Card is ChargeTurnGhoul);
	}
}
