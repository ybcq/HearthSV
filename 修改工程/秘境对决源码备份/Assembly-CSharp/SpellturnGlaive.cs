using System;
using System.Collections;
using System.Linq;

public class SpellturnGlaive : WeaponCard
{
	public SpellturnGlaive()
	{
		this.Name = "咏唱：异端审判";
		this.Description = "Count 1. Deathrattle: Kill a random enemy minion.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 2;
		this.BaseAttack = 0;
		this.BaseDurability = 1;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeWeapon();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		Minion minion = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
		where m.IsAlive()
		select m).ToList<Minion>());
		if (minion != null)
		{
			yield return minion.Destroy();
		}
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			int currentDurability = this.Weapon.CurrentDurability;
			this.Weapon.CurrentDurability = currentDurability - 1;
			yield return this.Weapon.CurrentDurability;
			if (this.Weapon.CurrentDurability <= 0)
			{
				yield return this.Player.DestroyWeapon();
			}
		}
		yield break;
	}
}
