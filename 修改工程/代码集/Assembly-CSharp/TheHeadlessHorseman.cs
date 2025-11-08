using System;
using System.Collections;

public class TheHeadlessHorseman : MinionCard
{
	public TheHeadlessHorseman()
	{
		this.Name = "无头骑士";
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
		int num;
		for (int i = 0; i < 3; i = num + 1)
		{
			yield return this.Player.Enemy.AddCardToHand(new AcherusDeathcharger());
			num = i;
		}
		yield break;
	}
}
