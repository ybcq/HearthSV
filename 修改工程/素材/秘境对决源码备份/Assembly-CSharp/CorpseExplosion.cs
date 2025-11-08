using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorpseExplosion : SpellCard
{
	public CorpseExplosion()
	{
		this.Name = "尸爆";
		this.Description = "Give a friendly minion Deathrattle: Deal 2 damage to all enemy minions.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		target.Controller.UpdateSprites();
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator Deathrattle(Minion self)
	{
		List<Minion> aliveMinions = (from m in self.Player.Enemy.Minions
		where m.IsAlive()
		select m).ToList<Minion>();
		foreach (Minion minion in aliveMinions)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, 2);
			yield return minion.Damage(null, 2);
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		foreach (Minion minion2 in aliveMinions)
		{
			yield return minion2.CheckDeath();
		}
		enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}
}
