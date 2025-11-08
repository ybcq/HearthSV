using System;
using System.Collections;

public class FrostwolfSavage : MinionCard
{
	public FrostwolfSavage()
	{
		this.Name = "Frostwolf Savage";
		this.Description = "Battlecry: Deal 3 damage to both heroes.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
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
		yield return this.Player.Enemy.Hero.Damage(null, 3);
		yield return this.Player.Hero.Damage(null, 3);
		yield return this.Player.Enemy.Hero.CheckDeath();
		yield return this.Player.Hero.CheckDeath();
		yield break;
	}
}
