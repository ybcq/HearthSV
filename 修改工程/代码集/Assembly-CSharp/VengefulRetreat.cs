using System;
using System.Collections;

public class VengefulRetreat : SpellCard
{
	public VengefulRetreat()
	{
		this.Name = "虹色光辉";
		this.Description = "Return a friendly minion to your hand. Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count > 0;
	}

	public override bool CanTarget(Character target)
	{
		return target != null && target.IsFriendlyOf(this.Player.Hero) && !target.HasSpellshield && target.IsMinion();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		MinionCard targetCard = minion.Card;
		yield return minion.ReturnToHand();
		yield return this.Player.Draw(null);
		targetCard.HasCharge = true;
		yield break;
	}
}
