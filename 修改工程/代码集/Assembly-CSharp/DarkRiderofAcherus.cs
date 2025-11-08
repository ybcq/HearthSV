using System;
using System.Collections;
using System.Linq;

public class DarkRiderofAcherus : MinionCard
{
	public DarkRiderofAcherus()
	{
		this.Name = "阿彻鲁斯黑暗骑士";
		this.Description = "Battlecry: Draw a Strike spell from your deck.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 5;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		BaseCard baseCard = RNG.RandomItemFrom<BaseCard>((from c in this.Player.Deck
		where c.GetTypeName().Contains("Strike")
		select c).ToList<BaseCard>());
		if (baseCard != null)
		{
			yield return this.Player.DrawFromDeck(baseCard, null);
		}
		yield break;
	}
}
