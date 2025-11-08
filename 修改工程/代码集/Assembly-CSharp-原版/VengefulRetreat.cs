using System;
using System.Collections;
using System.Linq;

public class VengefulRetreat : SpellCard
{
	public VengefulRetreat()
	{
		this.Name = "Vengeful Retreat";
		this.Description = "Return a friendly damaged minion to your hand. Give it Charge.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.IsDamaged());
	}

	public override bool CanTarget(Character target)
	{
		return target != null && target.IsFriendlyOf(this.Player.Hero) && !target.HasSpellshield && target.IsMinion() && target.IsDamaged();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		MinionCard targetCard = targetMinion.Card;
		yield return targetMinion.ReturnToHand();
		targetCard.HasCharge = true;
		yield break;
	}
}
