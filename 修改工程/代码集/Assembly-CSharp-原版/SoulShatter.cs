using System;
using System.Collections;
using System.Linq;

public class SoulShatter : SpellCard
{
	public SoulShatter()
	{
		this.Name = "Soul Shatter";
		this.Description = "Destroy your Soul Fragments. Restore 8 Health to your hero and draw a card for each.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Any((Minion m) => m.Card is SoulFragment);
	}

	public override IEnumerator Cast(Character target)
	{
		int destroyedFragments = 0;
		foreach (Minion fragmentMinion in (from m in this.Player.Minions
		where m.Card is SoulFragment
		select m).ToList<Minion>())
		{
			destroyedFragments++;
			yield return fragmentMinion.Destroy();
		}
		yield return this.Player.Hero.Heal(8 * destroyedFragments);
		yield return this.Player.Draw(destroyedFragments, null);
		yield break;
	}
}
