using System;
using System.Collections;
using UnityEngine;

public class CryoftheAbyss : WeaponCard
{
	public CryoftheAbyss()
	{
		this.Name = "咏唱：净魂之狐";
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
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			HighWarlordNajentus minionCard = new HighWarlordNajentus
			{
				BaseCost = 5,
				BaseAttack = 4,
				BaseHealth = 5,
				CurrentHealth = 5,
				HasTaunt = true
			};
			yield return this.Player.SummonMinion(minionCard);
			if (minionCard.Minion != null)
			{
				minionCard.Minion.Mechanics.RemoveAll();
			}
			minionCard = null;
			minionCard = null;
		}
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
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
