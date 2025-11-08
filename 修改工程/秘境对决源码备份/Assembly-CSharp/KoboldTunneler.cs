using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KoboldTunneler : MinionCard
{
	public KoboldTunneler()
	{
		this.Name = "瘟疫鼠";
		this.Description = "Deathrattle: A random opponent's creature gets -1/-1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where m.CurrentAttack >= 1 && m.Card.MinionType == MinionType.Biol
		select m).ToList<Minion>();
		if (list.Count > 0)
		{
			Character character = RNG.RandomItemFrom<Minion>(list);
			InterfaceManager.Instance.SpawnDamageSplatOn(character.Controller, 1);
			character.AddAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
			character.AddHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
			yield return character.CheckDeath();
		}
		yield break;
	}

	public int ApplyAttackModifier(int value)
	{
		return value - 1;
	}

	public int ApplyHealthModifier(int value)
	{
		return value - 1;
	}
}
