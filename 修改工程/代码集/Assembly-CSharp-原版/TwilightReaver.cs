using System;

public class TwilightReaver : MinionCard
{
	public TwilightReaver()
	{
		this.Name = "Twilight Reaver";
		this.Description = "Cleave";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.HasCleave = true;
		base.InitializeMinion();
	}
}
