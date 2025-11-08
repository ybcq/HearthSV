using System;
using System.Collections;

public class Shardling : MinionCard
{
	public Shardling()
	{
		this.Name = "默克布罗德·普蒂弗";
		this.Description = "Battlecry: Silence a friendly minion. Draw a card.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.FriendlyMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return this.Player.Minions.Count > 0;
	}

	public IEnumerator Battlecry(Character target)
	{
		target.As<Minion>().Silence();
		yield return this.Player.Draw(null);
		yield break;
	}
}
