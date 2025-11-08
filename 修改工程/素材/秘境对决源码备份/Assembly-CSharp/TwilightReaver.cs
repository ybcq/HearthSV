using System;

public class TwilightReaver : MinionCard
{
	public TwilightReaver()
	{
		this.Name = "龙人杀戮者";
		this.Description = "Cleave";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 4;
		this.HasCleave = true;
		base.InitializeMinion();
	}
}
