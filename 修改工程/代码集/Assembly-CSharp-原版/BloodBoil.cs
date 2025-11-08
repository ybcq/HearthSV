using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BloodBoil : SpellCard
{
	public BloodBoil()
	{
		this.Name = "Blood Boil";
		this.Description = "Choose a minion. Whenever it takes damage, deal 1 damage to all enemies.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().Mechanics.OnDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnDamaged));
		yield break;
	}

	public IEnumerator OnDamaged(MinionDamagedEvent evt)
	{
		List<Minion> enemyMinions = this.Player.Enemy.Minions.ToList<Minion>();
		foreach (Minion minion in enemyMinions)
		{
			yield return minion.Damage(null, 1);
		}
		foreach (Minion minion2 in enemyMinions)
		{
			yield return minion2.CheckDeath();
		}
		yield break;
	}
}
