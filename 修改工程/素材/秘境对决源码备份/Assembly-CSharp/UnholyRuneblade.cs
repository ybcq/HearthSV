using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class UnholyRuneblade : WeaponCard
{
	public UnholyRuneblade()
	{
		this.Name = "邪恶之刃";
		this.Description = "Deathrattle: Silence a random enemy minion.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseDurability = 2;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeWeapon();
	}

	public IEnumerator Deathrattle(Minion minion)
	{
		Minion minion2 = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
		where m.IsAlive() && m.Card.MinionType != MinionType.Totem
		select m).ToList<Minion>());
		if (minion2 != null)
		{
			minion2.Silence();
		}
		yield return new WaitForSeconds(0.25f);
		yield break;
	}
}
