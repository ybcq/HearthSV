using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Gyth : MinionCard
{
	public Gyth()
	{
		this.Name = "Gyth";
		this.Description = "Battlecry: Gain the Attack, Health, and effects of the last Dragon that was played.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 3;
		this.BaseAttack = 0;
		this.BaseHealth = 1;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		MinionCard lastDragon = this.Player.PlayedMinions.LastOrDefault((MinionCard m) => m.MinionType == MinionType.Dragon);
		if (lastDragon != null)
		{
			lastDragon = (MinionCard)lastDragon.Copy();
			this.GainedAttack = lastDragon.BaseAttack;
			this.Minion.AddAttackModifier(new Func<int, int>(this.GythAttackModifier));
			this.GainedHealth = lastDragon.BaseHealth - 1;
			this.Minion.AddHealthModifier(new Func<int, int>(this.GythHealthModifier));
			this.Minion.CurrentHealth = lastDragon.BaseHealth;
			this.Minion.Mechanics = lastDragon.Mechanics;
			this.Minion.CantAttack = lastDragon.CantAttack;
			this.Minion.CantAttackTaunt = lastDragon.CantAttackTaunt;
			this.Minion.HasFreeze = lastDragon.HasFreeze;
			this.Minion.HasTaunt = lastDragon.HasTaunt;
			this.Minion.HasCharge = lastDragon.HasCharge;
			this.Minion.HasPoison = lastDragon.HasPoison;
			this.Minion.HasWindfury = lastDragon.HasWindfury;
			this.Minion.HasDivineShield = lastDragon.HasDivineShield;
			this.Minion.HasSpellshield = lastDragon.HasSpellshield;
			this.Minion.IsEvasive = lastDragon.IsEvasive;
			this.Minion.IsInaccurate = lastDragon.IsInaccurate;
			this.Minion.IsStealth = lastDragon.IsStealth;
			this.Minion.SpellPower = lastDragon.SpellPower;
			yield return this.Minion.Mechanics.Battlecry.Fire(null);
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	private int GythAttackModifier(int attack)
	{
		return attack + this.GainedAttack;
	}

	private int GythHealthModifier(int health)
	{
		return health + this.GainedHealth;
	}

	private int GainedAttack;

	private int GainedHealth;
}
