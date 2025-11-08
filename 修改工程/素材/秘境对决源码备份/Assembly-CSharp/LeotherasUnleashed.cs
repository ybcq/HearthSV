using System;
using System.Collections;

public class LeotherasUnleashed : MinionCard
{
	public LeotherasUnleashed()
	{
		this.Name = "凯旋的骑士";
		this.Description = "Entry Song: Summons a heavy knight to the battlefield.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.RoyalGuard;
		this.BaseCost = 3;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	private IEnumerator Battlecry(Character target)
	{
		HighWarlordNajentus LeotherasUnleashedCard = new HighWarlordNajentus
		{
			BaseCost = 1,
			BaseAttack = 1,
			BaseHealth = 2,
			CurrentHealth = 2
		};
		yield return this.Player.SummonMinion(LeotherasUnleashedCard);
		if (LeotherasUnleashedCard.Minion != null)
		{
			LeotherasUnleashedCard.Minion.Mechanics.RemoveAll();
		}
		yield break;
	}
}
