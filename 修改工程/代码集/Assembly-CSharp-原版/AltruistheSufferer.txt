using System;
using System.Collections;
using System.Linq;

public class AltruistheSufferer : MinionCard
{
	public AltruistheSufferer()
	{
		this.Name = "Altruis the Sufferer";
		this.Description = "Battlecry: Give your other minions +2 Health. Exclude Kayn Sunfury from your deck.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanAddToDeck(SavedDeck deck)
	{
		return !deck.SavedCards.Any((SavedCard c) => c.Name == "KaynSunfury");
	}

	public IEnumerator Battlecry(Character target)
	{
		foreach (Minion minion in this.Player.Minions)
		{
			minion.CurrentHealth += 2;
			minion.AddHealthModifier(new Func<int, int>(this.AltruisModifier));
		}
		yield break;
	}

	public int AltruisModifier(int health)
	{
		return health + 2;
	}
}
