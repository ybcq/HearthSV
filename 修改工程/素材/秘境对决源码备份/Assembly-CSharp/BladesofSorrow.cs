using System;
using System.Collections;
using UnityEngine;

public class BladesofSorrow : WeaponCard
{
	public BladesofSorrow()
	{
		this.Name = "咏唱：白龙降临";
		this.Description = "Count 3. Deathrattle: Summon a wight Dragon";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 3;
		this.BaseAttack = 0;
		this.BaseDurability = 3;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeWeapon();
	}

	public IEnumerator Deathrattle(Minion evt)
	{
		HighWarlordNajentus BladesofSorrowCard = new HighWarlordNajentus
		{
			BaseCost = 6,
			BaseAttack = 6,
			BaseHealth = 6,
			CurrentHealth = 6
		};
		yield return this.Player.SummonMinion(BladesofSorrowCard);
		if (BladesofSorrowCard.Minion != null)
		{
			BladesofSorrowCard.Minion.Mechanics.RemoveAll();
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
