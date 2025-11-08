using System;
using System.Collections;
using System.Collections.Generic;

public class Burning : SpellCard
{
	public Burning()
	{
		this.Name = "冬之女王的即兴艺术";
		this.Description = "Transform all minions to snowmans.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		using (List<Minion>.Enumerator enumerator = GameManager.Instance.GetAllMinions().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				HighWarlordNajentus highWarlordNajentus = new HighWarlordNajentus();
				if (minion.Card.MinionType != MinionType.Totem)
				{
					minion.TransformInto(highWarlordNajentus);
				}
				if (highWarlordNajentus.Minion != null)
				{
					highWarlordNajentus.Minion.Mechanics.RemoveAll();
				}
			}
			yield break;
		}
		yield break;
	}
}
