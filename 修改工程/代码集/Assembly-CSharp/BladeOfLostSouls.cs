using System;
using System.Collections;

public class BladeOfLostSouls : WeaponCard
{
	public BladeOfLostSouls()
	{
		this.Name = "迷失之刃";
		this.Description = "Whenever your hero kills a minion, draw a card.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.BaseCost = 3;
		this.BaseAttack = 1;
		this.BaseDurability = 3;
		this.Mechanics.OnAttacked.Add(new Func<AttackedEvent, IEnumerator>(this.OnAttacked));
		base.InitializeWeapon();
	}

	public IEnumerator OnAttacked(AttackedEvent evt)
	{
		if (!evt.Target.IsAlive())
		{
			this.Weapon.Controller.As<WeaponController>().AnimateTriggerFlash();
			yield return this.Player.Draw(null);
		}
		yield break;
	}
}
