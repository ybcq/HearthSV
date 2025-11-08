using System;
using System.Collections;

public class CoilfangOracle : MinionCard
{
	public CoilfangOracle()
	{
		this.Name = "暗魔女将";
		this.Description = "Entry song: If the revenge state has been activated, you will get a gallop effect.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Vampire;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		if (this.Player.Hero.CurrentHealth <= 10)
		{
			this.Minion.HasCharge = true;
		}
		yield break;
	}
}
