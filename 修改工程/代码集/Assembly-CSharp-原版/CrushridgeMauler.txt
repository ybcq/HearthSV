using System;

public class CrushridgeMauler : MinionCard
{
	public CrushridgeMauler()
	{
		this.Name = "Crushridge Mauler";
		this.Description = "Windfury";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.HasWindfury = true;
		base.InitializeMinion();
	}
}
