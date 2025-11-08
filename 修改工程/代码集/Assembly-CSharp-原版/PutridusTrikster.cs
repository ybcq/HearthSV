using System;

public class PutridusTrikster : MinionCard
{
	public PutridusTrikster()
	{
		this.Name = "Putridus Trikster";
		this.Description = "Poison.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 5;
		this.HasPoison = true;
		base.InitializeMinion();
	}
}
