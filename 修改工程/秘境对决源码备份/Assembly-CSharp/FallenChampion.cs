using System;
using System.Collections;
using System.Linq;

public class FallenChampion : MinionCard
{
	public FallenChampion()
	{
		this.Name = "骷髅法师";
		this.Description = "Battlecry: Add a copy of the last spell and minion you cast to your hand.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 7;
		this.BaseAttack = 5;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		SpellCard spellCard = this.Player.PlayedSpells.LastOrDefault<SpellCard>();
		MinionCard lastMinion = this.Player.PlayedMinions.LastOrDefault<MinionCard>();
		if (spellCard != null)
		{
			yield return this.Player.AddCardToHand(spellCard.Copy());
		}
		if (lastMinion != null)
		{
			yield return this.Player.AddCardToHand(lastMinion.Copy());
		}
		yield break;
	}
}
