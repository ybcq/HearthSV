using System;

public class ArthasMenethil : Hero
{
	public ArthasMenethil()
	{
		this.BaseHealth = 30;
		this.Class = HeroClass.DeathKnight;
	}

	public override BaseHeroPower GetDefaultHeroPower()
	{
		return new RaiseGhoul(this);
	}
}
