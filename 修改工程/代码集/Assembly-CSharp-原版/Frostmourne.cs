using System;
using System.Collections;

public class Frostmourne : WeaponCard
{
	public Frostmourne()
	{
		this.Name = "Frostmourne";
		this.Description = "Whenever this kills a minion, summon a 3/3 Fallen Champion with Taunt.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Legendary;
		this.Collectible = false;
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
			this.Weapon.Controller.AnimateTriggerFlash();
			yield return this.Player.SummonMinion(new FallenChampion());
		}
		yield break;
	}
}
