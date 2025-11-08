using System;
using System.Collections;

public class WarglaiveofAzzinoth : WeaponCard
{
	public WarglaiveofAzzinoth()
	{
		this.Name = "咏唱：神域守护者";
		this.Description = "Count 5. Battercry: reduce durability of the count of your enemy's minions. Deathrattle: Summon a Taunt Fox. ";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.BaseCost = 2;
		this.BaseAttack = 0;
		this.BaseDurability = 5;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeWeapon();
	}

	public IEnumerator Deathrattle(Minion evt)
	{
		HighWarlordNajentus WarglaiveofAzzinothCard = new HighWarlordNajentus
		{
			BaseCost = 5,
			BaseAttack = 4,
			BaseHealth = 5,
			CurrentHealth = 5,
			HasTaunt = true
		};
		yield return this.Player.SummonMinion(WarglaiveofAzzinothCard);
		if (WarglaiveofAzzinothCard.Minion != null)
		{
			WarglaiveofAzzinothCard.Minion.Mechanics.RemoveAll();
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

	public IEnumerator Battlecry(Character target)
	{
		this.Weapon.CurrentDurability = this.BaseDurability - this.Player.Enemy.Minions.Count;
		if (this.Player.Enemy.HasWeapon())
		{
			this.Weapon.CurrentDurability--;
		}
		if (this.Weapon.CurrentDurability <= 0)
		{
			yield return this.Player.DestroyWeapon();
		}
		yield break;
	}
}
