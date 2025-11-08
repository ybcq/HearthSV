using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Sheep : MinionCard
{
	public Sheep()
	{
		this.Name = "病娇";
		this.Description = "Evasion. Deathrattle: Give ALL Illidari Aspirants +2/+2 (wherever they are)";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.IsEvasive = true;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		foreach (Minion minion in from m in GameManager.Instance.GetAllMinions()
		where m.Card is Sheep
		select m)
		{
			minion.AddAttackModifier(new Func<int, int>(this.IllidariAspirantModifier));
			minion.CurrentHealth++;
			minion.AddHealthModifier(new Func<int, int>(this.IllidariAspirantModifier));
		}
		foreach (IllidariAspirant illidariAspirant in GameManager.Instance.GetAllHandCards().OfType<IllidariAspirant>())
		{
			illidariAspirant.AddAttackModifier(new Func<int, int>(this.IllidariAspirantModifier));
			illidariAspirant.CurrentHealth += 2;
			illidariAspirant.AddHealthModifier(new Func<int, int>(this.IllidariAspirantModifier));
		}
		using (IEnumerator<IllidariAspirant> enumerator3 = GameManager.Instance.GetAllDeckCards().OfType<IllidariAspirant>().GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				IllidariAspirant illidariAspirant2 = enumerator3.Current;
				illidariAspirant2.AddAttackModifier(new Func<int, int>(this.IllidariAspirantModifier));
				illidariAspirant2.CurrentHealth += 2;
				illidariAspirant2.AddHealthModifier(new Func<int, int>(this.IllidariAspirantModifier));
			}
			yield break;
		}
		yield break;
	}

	public int IllidariAspirantModifier(int value)
	{
		return value + 2;
	}
}
