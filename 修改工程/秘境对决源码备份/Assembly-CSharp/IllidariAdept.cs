using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class IllidariAdept : MinionCard
{
	public IllidariAdept()
	{
		this.Name = "精灵驱逐者";
		this.Description = "Entry Song: Inflicts 1 random enemy's entourage 1 damage. This operation will be performed twice.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Elves;
		this.BaseCost = 6;
		this.BaseAttack = 4;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		Minion minion = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
		where m.IsAlive() && m.Card.MinionType != MinionType.Totem
		select m).ToList<Minion>());
		if (minion != null)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, 1);
			yield return minion.Damage(null, 1);
			yield return minion.CheckDeath();
		}
		Minion minion2 = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
		where m.IsAlive()
		select m).ToList<Minion>());
		if (minion2 != null)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, 1);
			yield return minion2.Damage(null, 1);
			yield return minion2.CheckDeath();
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
