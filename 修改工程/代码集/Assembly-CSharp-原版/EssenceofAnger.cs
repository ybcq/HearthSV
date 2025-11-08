using System;
using System.Collections;

public class EssenceofAnger : SpellCard
{
	public EssenceofAnger()
	{
		this.Name = "Essence of Anger";
		this.Description = "Give a minion +3 Attack.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().AddAttackModifier(new Func<int, int>(this.EssenceModifier));
		yield break;
	}

	public int EssenceModifier(int attack)
	{
		return attack + 3;
	}
}
