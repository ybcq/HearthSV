using System;

public class SnowLeopard : MinionCard
{
	public SnowLeopard()
	{
		this.Name = "Snow Leopard";
		this.Description = "Stealth";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.IsStealth = true;
		base.InitializeMinion();
	}
}
