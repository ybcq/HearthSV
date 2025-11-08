using System;
using System.Collections;

public class EssenceofAnger : SpellCard
{
	public EssenceofAnger()
	{
		this.Name = "利刃附魔";
		this.Description = "Give a minion +2 Attack.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().AddAttackModifier(new Func<int, int>(this.EssenceModifier));
		yield break;
	}

	public int EssenceModifier(int attack)
	{
		return attack + 2;
	}
}
