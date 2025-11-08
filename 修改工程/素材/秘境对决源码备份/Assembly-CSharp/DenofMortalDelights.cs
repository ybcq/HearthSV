using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DenofMortalDelights : SpellCard
{
	public DenofMortalDelights()
	{
		this.Name = "死亡祝福";
		this.Description = "Summon three zombies. Necromancer 6; Gives all zombies +0/+1 and guardian effects.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 6;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		HighWarlordNajentus DenofMortalDelightsCard = new HighWarlordNajentus
		{
			BaseCost = 2,
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		};
		yield return this.Player.SummonMinion(DenofMortalDelightsCard);
		if (DenofMortalDelightsCard.Minion != null)
		{
			DenofMortalDelightsCard.Minion.Mechanics.RemoveAll();
		}
		HighWarlordNajentus DenofMortalDelightsCard2 = new HighWarlordNajentus
		{
			BaseCost = 2,
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		};
		yield return this.Player.SummonMinion(DenofMortalDelightsCard2);
		if (DenofMortalDelightsCard2.Minion != null)
		{
			DenofMortalDelightsCard2.Minion.Mechanics.RemoveAll();
		}
		HighWarlordNajentus DenofMortalDelightsCard3 = new HighWarlordNajentus
		{
			BaseCost = 2,
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		};
		yield return this.Player.SummonMinion(DenofMortalDelightsCard3);
		if (DenofMortalDelightsCard3.Minion != null)
		{
			DenofMortalDelightsCard3.Minion.Mechanics.RemoveAll();
		}
		if (this.Player.DeadMinions.Count >= 6)
		{
			foreach (Minion minion in (from m in this.Player.Minions
			where m.Card is HighWarlordNajentus
			select m).ToList<Minion>())
			{
				minion.AddHealthModifier(new Func<int, int>(this.NajentusHealthModifier));
				minion.CurrentHealth++;
				yield return minion.HasTaunt = true;
			}
			List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
			yield break;
		}
		yield break;
		yield break;
	}

	public int NajentusHealthModifier(int health)
	{
		return health + 1;
	}
}
