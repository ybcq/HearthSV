using System;
using System.Collections;

public class IllidariAspirant : MinionCard
{
	public IllidariAspirant()
	{
		this.Name = "诅咒魔剑的吸血鬼";
		this.Description = "Entry song: If the revenge state is activated, you will get a +1/+1 effect.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Vampire;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public int IllidariAspirantModifier(int number)
	{
		return number + 1;
	}

	public IEnumerator Battlecry(Character target)
	{
		if (this.Player.Hero.CurrentHealth <= 10)
		{
			base.AddAttackModifier(new Func<int, int>(this.IllidariAspirantModifier));
			base.AddHealthModifier(new Func<int, int>(this.IllidariAspirantModifier));
			this.Minion.CurrentHealth++;
		}
		yield break;
	}
}
