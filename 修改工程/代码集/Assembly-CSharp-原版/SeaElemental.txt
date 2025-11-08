using System;
using System.Collections;

public class SeaElemental : MinionCard
{
	public SeaElemental()
	{
		this.Name = "Sea Elemental";
		this.Description = "Battlecry: Freeze the enemy hero.";
		this.Class = HeroClass.Neutral;
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
		this.Player.Enemy.Hero.Freeze();
		yield break;
	}
}
