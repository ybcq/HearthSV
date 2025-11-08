using System;
using System.Collections;
using System.Collections.Generic;

public class Despoil : SpellCard
{
	public Despoil()
	{
		this.Name = "掠夺";
		this.Description = "Give a minion a copy of ALL other minions' Deathrattle effects.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		using (List<Minion>.Enumerator enumerator = GameManager.Instance.GetAllMinions().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion2 = enumerator.Current;
				if (minion2 != minion && minion2.Mechanics.HasDeathrattle() && minion2.Card.MinionType != MinionType.Totem)
				{
					foreach (Func<Minion, IEnumerator> evt in minion2.Mechanics.Deathrattle.Events)
					{
						minion.Mechanics.Deathrattle.Add(evt);
					}
				}
			}
			yield break;
		}
		yield break;
	}
}
