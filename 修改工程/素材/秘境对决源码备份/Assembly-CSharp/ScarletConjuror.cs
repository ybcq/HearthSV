using System;
using System.Collections;
using System.Linq;

public class ScarletConjuror : MinionCard
{
	public ScarletConjuror()
	{
		this.Name = "姆诺兹多";
		this.Description = "Battlecry: Add a minion from your last opponent's warband to your hand.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		if (this.Player.Enemy.Hero.Class == HeroClass.DeathKnight)
		{
			MinionCard card = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
			where m.Class == HeroClass.Monk || m.Class == HeroClass.DemonHunter
			select m).ToList<MinionCard>());
			yield return this.Player.AddCardToHand(card);
			yield break;
		}
		if (this.Player.Enemy.Hero.Class == HeroClass.DemonHunter)
		{
			MinionCard card2 = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
			where m.Class == HeroClass.Monk || m.Class == HeroClass.DeathKnight
			select m).ToList<MinionCard>());
			yield return this.Player.AddCardToHand(card2);
			yield break;
		}
		MinionCard card3 = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
		where m.Class == HeroClass.DeathKnight || m.Class == HeroClass.DemonHunter
		select m).ToList<MinionCard>());
		yield return this.Player.AddCardToHand(card3);
		yield break;
	}
}
