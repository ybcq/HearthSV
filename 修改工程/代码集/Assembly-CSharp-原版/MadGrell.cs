using System;

public class MadGrell : MinionCard
{
	public MadGrell()
	{
		this.Name = "Mad Grell";
		this.Description = "Spell damage +1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.SpellPower = 1;
		base.InitializeMinion();
	}
}
