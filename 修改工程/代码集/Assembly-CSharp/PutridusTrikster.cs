using System;

public class PutridusTrikster : MinionCard
{
	public PutridusTrikster()
	{
		this.Name = "费洛蒙触手";
		this.Description = "Poison. Taunt";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 1;
		this.BaseAttack = 2;
		this.BaseHealth = 1;
		this.HasPoison = true;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
