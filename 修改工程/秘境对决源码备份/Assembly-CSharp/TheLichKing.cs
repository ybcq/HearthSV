using System;

public class TheLichKing : Hero
{
	public TheLichKing()
	{
		this.BaseHealth = 15;
		this.Class = HeroClass.DeathKnight;
	}

	public override BaseHeroPower GetDefaultHeroPower()
	{
		return new RaiseGhoul(this);
	}
}
