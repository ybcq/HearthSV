using System;
using System.Collections;
using System.Collections.Generic;

public class ScourgeStrike : SpellCard
{
	public ScourgeStrike()
	{
		this.Name = "天灾打击";
		this.Description = "Deal 5 damage. Trigger ALL Deathrattles.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 5 + this.Player.GetSpellPower();
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, damage);
		yield return target.Damage(null, damage);
		yield return target.CheckDeath();
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				yield return minion.Mechanics.Deathrattle.Fire(minion);
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		if (this.Player.HasWeapon())
		{
			WeaponCard card = this.Player.Weapon.Card;
			if (card.Mechanics.HasDeathrattle())
			{
				yield return card.Mechanics.Deathrattle.Fire(null);
			}
		}
		if (this.Player.Enemy.HasWeapon())
		{
			WeaponCard card2 = this.Player.Enemy.Weapon.Card;
			if (card2.Mechanics.HasDeathrattle())
			{
				yield return card2.Mechanics.Deathrattle.Fire(null);
			}
		}
		yield break;
		yield break;
	}
}
