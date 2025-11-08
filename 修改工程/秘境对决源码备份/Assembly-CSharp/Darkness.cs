using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Darkness : SpellCard
{
	public Darkness()
	{
		this.Name = "人偶师的线";
		this.Description = "Add 3 hanging silk puppet DarknessCards to your hand. Inflicts 1 damage to all enemies' entourage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		List<Minion> availableTargets = this.Player.Enemy.Minions.ToList<Minion>();
		foreach (Minion minion in availableTargets)
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, 1 + this.Player.GetSpellPower());
				yield return minion.Damage(null, 1 + this.Player.GetSpellPower());
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		foreach (Minion minion2 in availableTargets)
		{
			yield return minion2.CheckDeath();
		}
		enumerator = default(List<Minion>.Enumerator);
		HighWarlordNajentus card = new HighWarlordNajentus
		{
			BaseAttack = 1,
			BaseHealth = 1,
			BaseCost = 0,
			CurrentHealth = 1,
			HasCharge = true,
			CantAttackHeroes = true
		};
		yield return this.Player.AddCardToHand(card);
		HighWarlordNajentus card2 = new HighWarlordNajentus
		{
			BaseAttack = 1,
			BaseHealth = 1,
			BaseCost = 0,
			CurrentHealth = 1,
			HasCharge = true,
			CantAttackHeroes = true
		};
		yield return this.Player.AddCardToHand(card2);
		HighWarlordNajentus card3 = new HighWarlordNajentus
		{
			BaseAttack = 1,
			BaseHealth = 1,
			BaseCost = 0,
			CurrentHealth = 1,
			HasCharge = true,
			CantAttackHeroes = true
		};
		yield return this.Player.AddCardToHand(card3);
		yield break;
		yield break;
	}
}
