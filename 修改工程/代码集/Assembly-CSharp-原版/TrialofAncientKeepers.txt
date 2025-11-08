using System;
using System.Collections;

public class TrialofAncientKeepers : SpellCard
{
	public TrialofAncientKeepers()
	{
		this.Name = "Trial of Ancient Keepers";
		this.Description = "Shuffle a friendly minion into your deck and give it +3/+3. Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		yield return targetMinion.ReturnToDeck();
		targetMinion.Card.AddAttackModifier(new Func<int, int>(this.TrialofAncientKeepersModifier));
		targetMinion.Card.CurrentHealth += 3;
		targetMinion.Card.AddHealthModifier(new Func<int, int>(this.TrialofAncientKeepersModifier));
		yield return this.Player.Draw(null);
		yield break;
	}

	public int TrialofAncientKeepersModifier(int value)
	{
		return value + 3;
	}
}
