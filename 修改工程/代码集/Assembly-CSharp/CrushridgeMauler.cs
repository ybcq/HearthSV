using System;

public class CrushridgeMauler : MinionCard
{
	public CrushridgeMauler()
	{
		this.Name = "诸神之父迦拉克隆";
		this.Description = "Cleave.Windfury";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 8;
		this.BaseAttack = 5;
		this.BaseHealth = 300;
		this.HasCleave = true;
		this.HasWindfury = true;
		base.InitializeMinion();
	}
}
