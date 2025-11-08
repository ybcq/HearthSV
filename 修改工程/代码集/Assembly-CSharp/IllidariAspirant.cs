using System;
using System.Collections;
using System.Linq;

public class IllidariAspirant : MinionCard
{
	public IllidariAspirant()
	{
		this.Name = "Illidari Aspirant";
		this.Description = "Evasion. Deathrattle: Give ALL Illidari Aspirants +2/+2 (wherever they are)";
		this.Class = HeroClass.DemonHunter;
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
		where m.Card is IllidariAspirant
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
		foreach (IllidariAspirant illidariAspirant2 in GameManager.Instance.GetAllDeckCards().OfType<IllidariAspirant>())
		{
			illidariAspirant2.AddAttackModifier(new Func<int, int>(this.IllidariAspirantModifier));
			illidariAspirant2.CurrentHealth += 2;
			illidariAspirant2.AddHealthModifier(new Func<int, int>(this.IllidariAspirantModifier));
		}
		yield break;
	}

	public int IllidariAspirantModifier(int value)
	{
		return value + 2;
	}
}
