using System;
using System.Collections.Generic;

public static class ClassManager
{
	public static Dictionary<HeroClass, Type> Heroes = new Dictionary<HeroClass, Type>
	{
		{
			HeroClass.DeathKnight,
			typeof(ArthasMenethil)
		},
		{
			HeroClass.Monk,
			typeof(ChenStormstout)
		},
		{
			HeroClass.DemonHunter,
			typeof(VaredisFelsoul)
		}
	};

	public static Dictionary<HeroClass, Type> HeroPowers = new Dictionary<HeroClass, Type>
	{
		{
			HeroClass.DeathKnight,
			typeof(RaiseGhoul)
		},
		{
			HeroClass.Monk,
			typeof(ChiBurst)
		},
		{
			HeroClass.DemonHunter,
			typeof(DarkPact)
		}
	};
}
