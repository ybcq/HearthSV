using System;
using System.Collections;

public class DancingRuneWeapon : SpellCard
{
	public DancingRuneWeapon()
	{
		this.Name = "ÌøÎèµÄÎäÆ÷·ûÎÄ";
		this.Description = "Summon a minion with Attack and Health equal to your weapon's Attack and Durability.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.HasWeapon();
	}

	public override IEnumerator Cast(Character target)
	{
		DancingRuneblade minionCard = new DancingRuneblade
		{
			BaseAttack = this.Player.Weapon.CurrentAttack,
			BaseHealth = this.Player.Weapon.CurrentDurability,
			CurrentHealth = this.Player.Weapon.CurrentDurability
		};
		yield return this.Player.SummonMinion(minionCard);
		yield break;
	}
}