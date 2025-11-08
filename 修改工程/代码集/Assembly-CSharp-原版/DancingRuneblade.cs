using System;

public class DancingRuneblade : MinionCard
{
	public DancingRuneblade()
	{
		this.Name = "Dancing Runeblade";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Undead;
		this.Collectible = false;
		this.BaseCost = 1;
		base.InitializeMinion();
	}

	public int BattlecryModifierAmount;
}
