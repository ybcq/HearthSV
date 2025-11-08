using System;
using System.Collections;

public class Sporebat : MinionCard
{
	public Sporebat()
	{
		this.Name = "孢子蝠";
		this.Description = "Deathrattle: Give adjacent minions +1/+1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		foreach (Minion minion in self.Player.Minions)
		{
			if (minion.IsDeadNextTo(self))
			{
				minion.AddAttackModifier(new Func<int, int>(this.SporebatModifier));
				minion.CurrentHealth++;
				minion.AddHealthModifier(new Func<int, int>(this.SporebatModifier));
			}
		}
		yield break;
	}

	private int SporebatModifier(int value)
	{
		return value + 1;
	}
}
