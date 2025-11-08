using System;

public class CelebrianDryad : MinionCard
{
	public CelebrianDryad()
	{
		this.Name = "名人树精";
		this.Description = "Charge. Spellshield.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseHealth = 5;
		this.HasCharge = true;
		this.HasSpellshield = true;
		base.InitializeMinion();
	}
}
