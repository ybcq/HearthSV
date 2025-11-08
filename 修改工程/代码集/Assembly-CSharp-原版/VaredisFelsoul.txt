using System;

public class VaredisFelsoul : Hero
{
	public VaredisFelsoul()
	{
		this.BaseHealth = 30;
		this.Class = HeroClass.DemonHunter;
	}

	public override BaseHeroPower GetDefaultHeroPower()
	{
		return new DarkPact(this);
	}
}
