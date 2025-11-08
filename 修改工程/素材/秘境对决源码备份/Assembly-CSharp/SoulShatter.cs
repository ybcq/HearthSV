using System;
using System.Collections;

public class SoulShatter : SpellCard
{
	public SoulShatter()
	{
		this.Name = "暗黑供奉";
		this.Description = "Destroy a Friendly Minion. Restore Health equal to the minion's health to your hero and draw cards equal to the minion's health.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		int minionHealth = target.As<Minion>().CurrentHealth;
		yield return target.As<Minion>().Destroy();
		yield return this.Player.Hero.Heal(minionHealth);
		yield return this.Player.Draw(minionHealth, null);
		yield break;
	}
}
