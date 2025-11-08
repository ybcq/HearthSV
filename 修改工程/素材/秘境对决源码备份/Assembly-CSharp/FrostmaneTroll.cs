using System;

public class FrostmaneTroll : MinionCard
{
	public FrostmaneTroll()
	{
		this.Name = "武装平民";
		this.Description = "Taunt.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
