using System;
using System.Collections;

public class EssenceofDesire : SpellCard
{
	public EssenceofDesire()
	{
		this.Name = "血之契约";
		this.Description = "Take 2 damage，Draw 2 cards.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Hero.Damage(null, 2);
		yield return this.Player.Hero.CheckDeath();
		yield return this.Player.Draw(2, null);
		yield break;
	}
}
