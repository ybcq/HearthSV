using System;
using System.Collections;

public class GreenpawFurbolg : MinionCard
{
	public GreenpawFurbolg()
	{
		this.Name = "Greenpaw Furbolg";
		this.Description = "Battlecry: Restore 2 health.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.AllCharacters;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return target.Heal(2);
		yield return target.CheckDeath();
		yield break;
	}
}
