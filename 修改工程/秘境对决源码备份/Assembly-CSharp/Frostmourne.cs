using System;
using System.Collections;
using UnityEngine;

public class Frostmourne : WeaponCard
{
	public Frostmourne()
	{
		this.Name = "霜之哀伤";
		this.Description = "Whenever this kills a minion, summon a 3/3 Ghoul.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.BaseCost = 7;
		this.BaseAttack = 5;
		this.BaseDurability = 3;
		this.Mechanics.OnAttacked.Add(new Func<AttackedEvent, IEnumerator>(this.OnAttacked));
		base.InitializeWeapon();
	}

	public IEnumerator OnAttacked(AttackedEvent attackedEvent)
	{
		if (!attackedEvent.Target.IsAlive())
		{
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			ChargeTurnGhoul FrostmourneCard = new ChargeTurnGhoul
			{
				BaseCost = 3,
				BaseAttack = 3,
				BaseHealth = 3,
				CurrentHealth = 3
			};
			yield return this.Player.SummonMinion(FrostmourneCard);
			if (FrostmourneCard.Minion != null)
			{
				FrostmourneCard.Minion.HasCharge = false;
				FrostmourneCard.Minion.HasTaunt = true;
				FrostmourneCard.Minion.Mechanics.RemoveAll();
			}
			FrostmourneCard = null;
			FrostmourneCard = null;
		}
		yield break;
	}
}
