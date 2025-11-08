using System;
using System.Collections;
using System.Collections.Generic;

public class FireElemental : MinionCard
{
	public FireElemental()
	{
		this.Name = "红衣纳迪娜";
		this.Description = "Deathrattle: Give your Dragons Divine Shield.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 7;
		this.BaseHealth = 4;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion evt)
	{
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion.Card.MinionType == MinionType.Dragon)
			{
				yield return minion.HasDivineShield = true;
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
