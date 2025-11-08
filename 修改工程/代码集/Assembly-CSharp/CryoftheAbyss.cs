using System;
using System.Collections;

public class CryoftheAbyss : WeaponCard
{
	public CryoftheAbyss()
	{
		this.Name = "咏唱：净化之狐";
		this.Description = "Count 1. Deathrattle: Summon a Taunt Fox.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 4;
		this.BaseAttack = 0;
		this.BaseDurability = 1;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeWeapon();
	}

	public IEnumerator Deathrattle(Minion minion)
	{
		if (this.Player.Minions.Count < 7)
		{
			yield return this.Player.SummonMinion(new FireElemental());
			yield break;
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
		}
		yield break;
	}
}
