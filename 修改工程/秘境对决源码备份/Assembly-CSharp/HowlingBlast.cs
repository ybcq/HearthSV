using System;
using System.Collections;
using System.Collections.Generic;

public class HowlingBlast : SpellCard
{
	public HowlingBlast()
	{
		this.Name = "尖叫爆炸";
		this.Description = "Destroy all Frozen minions, then Freeze all minions.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion.IsFrozen && minion.Card.MinionType != MinionType.Totem)
			{
				yield return minion.Destroy();
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		using (List<Minion>.Enumerator enumerator2 = GameManager.Instance.GetAllMinions().GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				Minion minion2 = enumerator2.Current;
				if (minion2.Card.MinionType != MinionType.Totem)
				{
					minion2.Freeze();
				}
			}
			yield break;
		}
		yield break;
		yield break;
	}
}
