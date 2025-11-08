using System;
using System.Collections;
using System.Linq;

public class Brightwing : MinionCard
{
	public Brightwing()
	{
		this.Name = "阿什顿格先知";
		this.Description = "Battlecry: Your opponent draws a spell. It costs (4) more.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		SpellCard spellCard = RNG.RandomItemFrom<SpellCard>(this.Player.Enemy.Deck.OfType<SpellCard>().ToList<SpellCard>());
		if (spellCard != null)
		{
			spellCard.AddCostModifier(new Func<int, int>(this.AshtongueSeerModifier));
			yield return this.Player.Enemy.DrawFromDeck(spellCard, null);
		}
		yield break;
	}

	public int AshtongueSeerModifier(int cost)
	{
		return cost + 4;
	}
}
