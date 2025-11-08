using System;

public class SoulFragment : MinionCard
{
	public SoulFragment()
	{
		this.Name = "Soul Fragment";
		this.Description = "Can't attack. Spellshield. Taunt.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 0;
		this.BaseAttack = 0;
		this.BaseHealth = 1;
		this.Collectible = false;
		this.CantAttack = true;
		this.HasSpellshield = true;
		this.HasTaunt = true;
		base.InitializeMinion();
	}
}
