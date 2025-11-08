using System;
using System.Collections;

public class AshtongueSeer : MinionCard
{
	public AshtongueSeer()
	{
		this.Name = "新星魔术师·萨米";
		this.Description = "Battlecry: You and your opponent draws a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.Enemy.Draw(null);
		yield return this.Player.Draw(null);
		yield break;
	}
}
