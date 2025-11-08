using System;

public class ElwynnForestBear : MinionCard
{
	public ElwynnForestBear()
	{
		this.Name = "光辉之门";
		this.Description = "Taunt, Can't Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 7;
		this.CantAttack = true;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
