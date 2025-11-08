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
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeWeapon();
	}

	public IEnumerable Battlecry(Character target)
	{
		int currentDurability = this.Weapon.CurrentDurability;
		this.Weapon.CurrentDurability = currentDurability - this.Player.Enemy.Minions.Count;
		yield return this.Weapon.CurrentDurability;
		yield break;
	}

	public IEnumerator Deathrattle(Minion evt)
	{
		yield return this.Player.SummonMinion(new FireElemental());
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
