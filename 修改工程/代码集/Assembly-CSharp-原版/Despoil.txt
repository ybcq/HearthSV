using System;
using System.Collections;

public class Despoil : SpellCard
{
	public Despoil()
	{
		this.Name = "Despoil";
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
		foreach (Minion minion2 in GameManager.Instance.GetAllMinions())
		{
			if (minion2 != minion && minion2.Mechanics.HasDeathrattle())
			{
				foreach (Func<Minion, IEnumerator> evt in minion2.Mechanics.Deathrattle.Events)
				{
					minion.Mechanics.Deathrattle.Add(evt);
				}
			}
		}
		yield break;
	}
}
