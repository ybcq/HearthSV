using System;
using System.Collections;
using System.Collections.Generic;

public class SigilofChains : SpellCard
{
	public SigilofChains()
	{
		this.Name = "回归根源";
		this.Description = "Return the followers of both parties to their respective hands.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 7;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				yield return minion.ReturnToHand();
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
