using System;
using System.Collections;

public class TheHeadlessHorseman : MinionCard
{
	public TheHeadlessHorseman()
	{
		this.Name = "The Headless Horseman";
		this.Description = "Battlecry: Give your opponent 3 Flaming Pumpkins.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 9;
		this.BaseAttack = 8;
		this.BaseHealth = 8;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		for (int i = 0; i < 3; i++)
		{
			yield return this.Player.Enemy.AddCardToHand(new FlamingPumpkin());
		}
		yield break;
	}
}
