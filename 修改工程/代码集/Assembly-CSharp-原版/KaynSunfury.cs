using System;
using System.Collections;
using System.Linq;

public class KaynSunfury : MinionCard
{
	public KaynSunfury()
	{
		this.Name = "Kayn Sunfury";
		this.Description = "Battlecry: Give your other minions +2 Attack. Exclude Altruis the Sufferer from your deck.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanAddToDeck(SavedDeck deck)
	{
		return !deck.SavedCards.Any((SavedCard c) => c.Name == "AltruistheSufferer");
	}

	public IEnumerator Battlecry(Character target)
	{
		foreach (Minion minion in this.Player.Minions)
		{
			minion.AddAttackModifier(new Func<int, int>(this.KaynModifier));
		}
		yield break;
	}

	public int KaynModifier(int attack)
	{
		return attack + 2;
	}
}
