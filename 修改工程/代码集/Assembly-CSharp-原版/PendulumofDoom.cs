using System;
using System.Collections;
using UnityEngine;

public class PendulumofDoom : WeaponCard
{
	public PendulumofDoom()
	{
		this.Name = "Pendulum of Doom";
		this.Description = "Whenever a friendly minion dies, gain +1 Attack. Whenever an enemy minion dies, lose 1 Attack.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseDurability = 3;
		this.Mechanics.OnMinionDied.Add(new Func<MinionDiedEvent, IEnumerator>(this.OnMinionDied));
		base.InitializeWeapon();
	}

	public IEnumerator OnMinionDied(MinionDiedEvent evt)
	{
		this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.5f);
		if (evt.Minion.IsFriendlyOf(this.Player.Hero))
		{
			this.Weapon.AddAttackModifier(new Func<int, int>(this.PendulumFriendlyModifier));
		}
		else
		{
			this.Weapon.AddAttackModifier(new Func<int, int>(this.PendulumEnemyModifier));
		}
		yield break;
	}

	public int PendulumFriendlyModifier(int attack)
	{
		return attack + 1;
	}

	public int PendulumEnemyModifier(int attack)
	{
		return attack + 1;
	}
}
