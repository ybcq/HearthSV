using System;
using System.Collections;

public class SheWhoPlots : MinionCard
{
	public SheWhoPlots()
	{
		this.Name = "伟大的女修士";
		this.Description = "Entry Song: Regenerate 5 points of life for your main warrior or one of your followers.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Pope;
		this.BaseCost = 7;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.FriendlyCharacters;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return target.Heal(5);
		yield break;
	}
}
