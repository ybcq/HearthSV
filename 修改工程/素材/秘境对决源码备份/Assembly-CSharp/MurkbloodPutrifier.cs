using System;
using System.Collections;

public class MurkbloodPutrifier : MinionCard
{
	public MurkbloodPutrifier()
	{
		this.Name = "银白幼龙";
		this.Description = "Battlecry: if your hero'turnmana is more than 10, Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		if (this.Player.TurnMana >= 7)
		{
			yield return this.Player.Draw(null);
		}
		yield break;
	}
}
