using System;
using System.Collections;
using System.Linq;

public class VengefulRetreat : SpellCard
{
	public VengefulRetreat()
	{
		this.Name = "虹色光辉";
		this.Description = "Return a friendly minion to your hand. Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Any((Minion m) => m.As<Minion>().Card.BaseCost < 3);
	}

	public override bool CanTarget(Character target)
	{
		return target != null && (target.IsFriendlyOf(this.Player.Hero) || !target.IsStealth) && !target.HasSpellshield && target.IsMinion() && target.As<Minion>().Card.BaseCost < 3;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		MinionCard card = minion.Card;
		card.AttackModifiers.Clear();
		card.HealthModifiers.Clear();
		if (card.CurrentHealth > card.MaxHealth)
		{
			card.CurrentHealth = card.MaxHealth;
		}
		yield return minion.ReturnToHand();
		yield return this.Player.Draw(null);
		yield break;
	}
}
