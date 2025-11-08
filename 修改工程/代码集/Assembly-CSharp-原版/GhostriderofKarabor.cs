using System;

public class GhostriderofKarabor : MinionCard
{
	public GhostriderofKarabor()
	{
		this.Name = "Ghostrider of Karabor";
		this.Description = "Charge.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Collectible = false;
		this.HasCharge = true;
		base.InitializeMinion();
	}
}
