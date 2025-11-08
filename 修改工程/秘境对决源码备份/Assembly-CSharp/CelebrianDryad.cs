using System;
using System.Collections;

public class CelebrianDryad : MinionCard
{
	public CelebrianDryad()
	{
		this.Name = "行船商人";
		this.Description = "Battlecry: Both sides draw a card.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.Draw(null);
		yield return this.Player.Enemy.Draw(null);
		yield break;
	}
}
