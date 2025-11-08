using System;
using System.Collections;

public class Coin : SpellCard
{
	public Coin()
	{
		this.Name = "Coin";
		this.Description = "Gain 1 Mana Crystal this turn only.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.AddTurnMana(1);
		yield break;
	}
}
