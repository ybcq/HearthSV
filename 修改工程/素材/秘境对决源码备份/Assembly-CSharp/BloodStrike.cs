using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BloodStrike : SpellCard
{
	public BloodStrike()
	{
		this.Name = "鲜血打击";
		this.Description = "Kill all your Ghouls. Whenever you kill one, damage 1 to the most center enemy.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int destroyedFragments = 0;
		foreach (Minion minion in (from m in this.Player.Minions
		where m.Card is ChargeTurnGhoul
		select m).ToList<Minion>())
		{
			int num = destroyedFragments;
			destroyedFragments = num + 1;
			yield return minion.Destroy();
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		int num2;
		for (int i = 1; i < destroyedFragments; i = num2 + 1)
		{
			if (this.Player.Enemy.Minions.Count % 2 == 1)
			{
				int middlePosition = (int)Mathf.Floor((float)this.Player.Enemy.Minions.Count / 2f);
				InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Minions[middlePosition].Controller, 1 + this.Player.GetSpellPower());
				yield return this.Player.Enemy.Minions[middlePosition].Damage(null, 1 + this.Player.GetSpellPower());
				yield return this.Player.Enemy.Minions[middlePosition].CheckDeath();
			}
			else
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 1 + this.Player.GetSpellPower());
				yield return this.Player.Enemy.Hero.Damage(null, 1 + this.Player.GetSpellPower());
				yield return this.Player.Enemy.Hero.CheckDeath();
			}
			num2 = i;
		}
		yield break;
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Any((Minion m) => m.Card is ChargeTurnGhoul);
	}
}
