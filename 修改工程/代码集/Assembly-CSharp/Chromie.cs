using System;
using System.Collections;
using System.Linq;

public class Chromie : MinionCard
{
	public Chromie()
	{
		this.Name = "克罗米";
		this.Description = "Battlecry: Add a copy of the last spell and minion you cast to your hand.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 7;
		this.BaseAttack = 5;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		SpellCard lastSpell = this.Player.PlayedSpells.LastOrDefault<SpellCard>();
		MinionCard lastMinion = this.Player.PlayedMinions.LastOrDefault<MinionCard>();
		if (lastSpell != null)
		{
			yield return this.Player.AddCardToHand(lastSpell.Copy());
		}
		if (lastMinion != null)
		{
			yield return this.Player.AddCardToHand(lastMinion.Copy());
		}
		yield break;
	}
}
