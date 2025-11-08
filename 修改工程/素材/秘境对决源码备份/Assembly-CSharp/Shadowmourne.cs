using System;
using System.Collections;

public class Shadowmourne : WeaponCard
{
	public Shadowmourne()
	{
		this.Name = "暗影哀悼";
		this.Description = "Battlecry: Destroy a friendly minion and gain its Attack.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.BaseCost = 4;
		this.BaseAttack = 0;
		this.BaseDurability = 2;
		this.BattlecryType = BattlecryType.FriendlyMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeWeapon();
	}

	public override bool CanBattlecry()
	{
		return this.Player.Minions.Count > 0;
	}

	public IEnumerator Battlecry(Character target)
	{
		if (target != null)
		{
			this.GainedAttack = target.CurrentAttack;
			yield return target.As<Minion>().Destroy();
			this.Player.Weapon.AddAttackModifier(new Func<int, int>(this.ShadowmourneModifier));
		}
		yield break;
	}

	public int ShadowmourneModifier(int attack)
	{
		return attack + this.GainedAttack;
	}

	public int GainedAttack;
}
