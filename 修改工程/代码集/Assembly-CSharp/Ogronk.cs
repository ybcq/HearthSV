using System;

public class Ogronk : MinionCard
{
	public Ogronk()
	{
		this.Name = "救死扶伤僧";
		this.Description = "Evasion. Inaccurate.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.IsEvasive = true;
		this.IsInaccurate = true;
		base.InitializeMinion();
	}
}
