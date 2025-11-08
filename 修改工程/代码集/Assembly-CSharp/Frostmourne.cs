using System;
using System.Collections;

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
			ChargeTurnGhoul frozenGhoul = new ChargeTurnGhoul
			{
				BaseAttack = 3,
				BaseHealth = 3,
				CurrentHealth = 3
			};
			yield return this.Player.SummonMinion(frozenGhoul);
			if (frozenGhoul.Minion != null)
			{
				frozenGhoul.Minion.Silence();
				frozenGhoul.Minion.HasTaunt = true;
			}
			frozenGhoul = null;
			frozenGhoul = null;
		}
		yield break;
	}
}
