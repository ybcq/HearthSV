using System;
using System.Collections;

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
			if (minion.IsFrozen)
			{
				yield return minion.Destroy();
			}
		}
		foreach (Minion minion2 in GameManager.Instance.GetAllMinions())
		{
			minion2.Freeze();
		}
		yield break;
	}
}
