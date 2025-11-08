using System;
using System.Collections;
using System.Linq;

public class RitualofBinding : SpellCard
{
	public RitualofBinding()
	{
		this.Name = "Ritual of Binding";
		this.Description = "Give a friendly non-Demon minion +4/+4 and Inaccurate.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.Card.MinionType != MinionType.Demon);
	}

	public override bool CanTarget(Character target)
	{
		return target != null && target.IsFriendlyOf(this.Player.Hero) && !target.HasSpellshield && target.IsMinion() && target.As<Minion>().Card.MinionType != MinionType.Demon;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		minion.AddAttackModifier(new Func<int, int>(this.RitualofBindingModifier));
		minion.CurrentHealth += 4;
		minion.AddHealthModifier(new Func<int, int>(this.RitualofBindingModifier));
		minion.IsInaccurate = true;
		yield break;
	}

	public int RitualofBindingModifier(int value)
	{
		return value + 4;
	}
}
