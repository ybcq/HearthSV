using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoulCleave : SpellCard
{
	public SoulCleave()
	{
		this.Name = "炼金术的知识";
		this.Description = "Deal 3 damage to all enemy minions.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 7;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<Minion> list = this.Player.Enemy.Minions.ToList<Minion>();
		foreach (Minion minion in list)
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, 3 + this.Player.GetSpellPower());
				yield return minion.Damage(null, 3 + this.Player.GetSpellPower());
				yield return minion.CheckDeath();
			}
			minion = null;
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
