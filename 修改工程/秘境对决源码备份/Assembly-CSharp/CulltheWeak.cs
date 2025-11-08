using System;
using System.Collections;
using System.Collections.Generic;

public class CulltheWeak : SpellCard
{
	public CulltheWeak()
	{
		this.Name = "忒弥斯的审判";
		this.Description = "Destroy all minions.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 6;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				yield return minion.Destroy();
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
