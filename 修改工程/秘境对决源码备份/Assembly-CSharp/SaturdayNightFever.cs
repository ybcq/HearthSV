using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SaturdayNightFever : SpellCard
{
	public SaturdayNightFever()
	{
		this.Name = "周末狂欢";
		this.Description = "At the start of your next turn, deal 2 damage to all non-Undead minions.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.TurnStartSubscription = EventManager.Instance.TurnStartHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.TurnStartSubscription.Dispose();
			List<Minion> targetMinions = (from m in GameManager.Instance.GetAllMinions()
			where m.Card.MinionType != MinionType.Undead
			select m).ToList<Minion>();
			int damage = 2 + this.Player.GetSpellPower();
			foreach (Minion minion in targetMinions)
			{
				if (minion.Card.MinionType != MinionType.Totem)
				{
					InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, damage);
					yield return minion.Damage(null, damage);
				}
			}
			List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
			foreach (Minion minion2 in targetMinions)
			{
				yield return minion2.CheckDeath();
			}
			enumerator = default(List<Minion>.Enumerator);
			targetMinions = null;
			targetMinions = null;
			targetMinions = null;
		}
		yield break;
		yield break;
	}

	public IDisposable TurnStartSubscription;
}
