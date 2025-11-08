using System;
using System.Collections;

public class BloodscaleWavecaller : MinionCard
{
	public BloodscaleWavecaller()
	{
		this.Name = "高阶牧师";
		this.Description = "Entry song: Decrease all your amulets by 1 countdown.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Pope;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return this.Player.HasWeapon();
	}

	public IEnumerator Battlecry(Character target)
	{
		Weapon weapon = this.Player.Weapon;
		int currentDurability = weapon.CurrentDurability - 1;
		yield return weapon.CurrentDurability = currentDurability;
		if (this.Player.Weapon.CurrentDurability <= 0)
		{
			yield return this.Player.DestroyWeapon();
		}
		yield break;
	}
}
