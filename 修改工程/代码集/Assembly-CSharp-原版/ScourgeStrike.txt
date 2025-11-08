using System;
using System.Collections;

public class ScourgeStrike : SpellCard
{
	public ScourgeStrike()
	{
		this.Name = "Scourge Strike";
		this.Description = "Deal 5 damage. Trigger ALL Deathrattles.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 5 + this.Player.GetSpellPower();
		yield return target.Damage(null, damage);
		yield return target.CheckDeath();
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			yield return minion.Mechanics.Deathrattle.Fire(minion);
		}
		if (this.Player.HasWeapon())
		{
			WeaponCard playerWeapon = this.Player.Weapon.Card;
			if (playerWeapon.Mechanics.HasDeathrattle())
			{
				yield return playerWeapon.Mechanics.Deathrattle.Fire(null);
			}
		}
		if (this.Player.Enemy.HasWeapon())
		{
			WeaponCard playerWeapon2 = this.Player.Enemy.Weapon.Card;
			if (playerWeapon2.Mechanics.HasDeathrattle())
			{
				yield return playerWeapon2.Mechanics.Deathrattle.Fire(null);
			}
		}
		yield break;
	}
}
