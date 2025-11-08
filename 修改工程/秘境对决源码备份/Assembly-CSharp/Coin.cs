using System;
using System.Collections;

public class Coin : SpellCard
{
	public Coin()
	{
		this.Name = "加里维克斯的幸运币";
		this.Description = "Gain 1 Mana Crystal this turn only.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		this.Collectible = false;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.AddTurnMana(1);
		yield break;
	}
}
