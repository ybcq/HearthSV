using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChaosNova : SpellCard
{
	public ChaosNova()
	{
		this.Name = "腐坏飓风";
		this.Description = "Deal 1 damage to all enemy minions. Necromancy 6; The original 1 damage is changed to 3 damage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 0;
		if (this.Player.DeadMinions.Count < 6)
		{
			damage = 1 + this.Player.GetSpellPower();
		}
		else
		{
			damage = 3 + this.Player.GetSpellPower();
		}
		List<Minion> targetMinions = this.Player.Enemy.Minions.ToList<Minion>();
		foreach (Minion minion in targetMinions)
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, damage);
				yield return minion.Damage(null, damage);
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		foreach (Minion minion2 in targetMinions)
		{
			yield return minion2.CheckDeath();
		}
		enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
