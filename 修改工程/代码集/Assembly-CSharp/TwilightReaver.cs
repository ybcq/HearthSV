using System;

public class TwilightReaver : MinionCard
{
	public TwilightReaver()
	{
		this.Name = "Twilight Reaver";
		this.Description = "Cleave";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 6;
		this.BaseAttack = 6;
		this.BaseHealth = 6;
		this.HasCleave = true;
		base.InitializeMinion();
	}
}
