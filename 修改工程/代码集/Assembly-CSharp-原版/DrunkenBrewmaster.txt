using System;
using System.Collections;

public class DrunkenBrewmaster : MinionCard
{
	public DrunkenBrewmaster()
	{
		this.Name = "Drunken Brewmaster";
		this.Description = "Battlecry: Give your hero a Tasty Brew.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.AddCardToHand(new TastyBrew());
		yield break;
	}
}
