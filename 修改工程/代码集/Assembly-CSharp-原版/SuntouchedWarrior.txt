using System;

public class SuntouchedWarrior : MinionCard
{
	public SuntouchedWarrior()
	{
		this.Name = "Suntouched Warrior";
		this.Description = "Divine Shield";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.HasDivineShield = true;
		base.InitializeMinion();
	}
}
