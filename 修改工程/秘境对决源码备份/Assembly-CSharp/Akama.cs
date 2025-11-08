using System;
using System.Collections;
using System.Collections.Generic;

public class Akama : MinionCard
{
	public Akama()
	{
		this.Name = "光迹通灵僧";
		this.Description = "Warcry: Shuffle the target player's graveyard back into the library.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.AllCharacters;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		using (List<MinionCard>.Enumerator enumerator = target.Player.DeadMinions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MinionCard card = enumerator.Current;
				target.Player.AddCardToDeck(card);
			}
			yield break;
		}
		yield break;
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && target.IsHero();
	}
}
