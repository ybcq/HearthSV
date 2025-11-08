using System;

public class ChenStormstout : Hero
{
	public ChenStormstout()
	{
		this.BaseHealth = 30;
		this.Class = HeroClass.Monk;
	}

	public override BaseHeroPower GetDefaultHeroPower()
	{
		return new ChiBurst(this);
	}
}
