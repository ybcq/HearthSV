using System;
using System.Collections;

public class SandfuryBloodDrinker : MinionCard
{
	public SandfuryBloodDrinker()
	{
		this.Name = "灰烬破灭狂徒";
		this.Description = "Warcry: The target artifact loses all durability, and this creature takes sustained damage.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.AllCharacters;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		if (target.Player.HasWeapon())
		{
			int damage = target.Player.Weapon.CurrentDurability;
			yield return target.Player.DestroyWeapon();
			InterfaceManager.Instance.SpawnDamageSplatOn(this.Minion.Controller, damage);
			yield return this.Minion.Damage(null, damage);
			yield return this.Minion.CheckDeath();
		}
		yield break;
	}

	public override bool CanBattlecry()
	{
		return this.Player.HasWeapon() || this.Player.Enemy.HasWeapon();
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && target.IsHero();
	}
}
