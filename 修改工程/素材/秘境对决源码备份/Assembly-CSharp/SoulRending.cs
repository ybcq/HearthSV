using System;
using System.Collections;

public class SoulRending : SpellCard
{
	public SoulRending()
	{
		this.Name = "妖精的恶作剧";
		this.Description = "Return one of your own followers to your hand, and randomly return an enemy's follower to the enemy's hand.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		yield return minion.ReturnToHand();
		Minion minion2 = RNG.RandomItemFrom<Minion>(this.Player.Enemy.Minions);
		if (minion2.Card.MinionType != MinionType.Totem)
		{
			yield return minion2.ReturnToHand();
		}
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}
}
