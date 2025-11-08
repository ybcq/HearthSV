using System;
using System.Collections;

public class Soulseeker : WeaponCard
{
	public Soulseeker()
	{
		this.Name = "Soulseeker";
		this.Description = "Whenever your hero attacks and kills a minion, summon a Soul Fragment.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseDurability = 3;
		this.Mechanics.OnHeroAttacked.Add(new Func<HeroAttackedEvent, IEnumerator>(this.OnHeroAttacked));
		base.InitializeWeapon();
	}

	private IEnumerator OnHeroAttacked(HeroAttackedEvent evt)
	{
		if (evt.Hero == this.Player.Hero && !evt.Target.IsAlive())
		{
			yield return this.Player.SummonMinion(new SoulFragment());
		}
		yield break;
	}
}
