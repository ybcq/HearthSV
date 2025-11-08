using System;
using System.Collections;
using System.Linq;

public class GoldshirePatrol : MinionCard
{
	public GoldshirePatrol()
	{
		this.Name = "չ�ݲ豭";
		this.Description = "Battlecry: Give 3 random friendly minions of different minion types +1/+1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public int GoldshirePatrolModifier(int value)
	{
		return value + 1;
	}

	public IEnumerator Battlecry(Character target)
	{
		MinionType Minion1 = MinionType.Demon;
		MinionType Minion2 = MinionType.Demon;
		Minion randomMinion = RNG.RandomItemFrom<Minion>((from m in this.Player.Minions
		where m.IsAlive()
		select m).ToList<Minion>());
		if (randomMinion != null)
		{
			randomMinion.AddAttackModifier(new Func<int, int>(this.GoldshirePatrolModifier));
			randomMinion.AddHealthModifier(new Func<int, int>(this.GoldshirePatrolModifier));
			randomMinion.CurrentHealth++;
			Minion1 = randomMinion.Card.MinionType;
		}
		Minion randomMinion2 = RNG.RandomItemFrom<Minion>((from m in this.Player.Minions
		where m.IsAlive() && m.Card.MinionType != Minion1
		select m).ToList<Minion>());
		if (randomMinion2 != null)
		{
			randomMinion2.AddAttackModifier(new Func<int, int>(this.GoldshirePatrolModifier));
			randomMinion2.AddHealthModifier(new Func<int, int>(this.GoldshirePatrolModifier));
			randomMinion2.CurrentHealth++;
			Minion2 = randomMinion2.Card.MinionType;
		}
		Minion randomMinion3 = RNG.RandomItemFrom<Minion>((from m in this.Player.Minions
		where m.IsAlive() && m.Card.MinionType != Minion1 && m.Card.MinionType != Minion2
		select m).ToList<Minion>());
		if (randomMinion3 != null)
		{
			randomMinion3.AddAttackModifier(new Func<int, int>(this.GoldshirePatrolModifier));
			randomMinion3.AddHealthModifier(new Func<int, int>(this.GoldshirePatrolModifier));
			randomMinion3.CurrentHealth++;
		}
		yield return true;
		yield break;
	}

	public override bool CanBattlecry()
	{
		return GameManager.Instance.GetAllMinions().Any((Minion m) => m.IsFriendlyOf(this.Player.Hero));
	}
}
