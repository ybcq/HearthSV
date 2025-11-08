using System;
using System.Collections;
using System.Linq;

public class BrawlingStance : SpellCard
{
	public BrawlingStance()
	{
		this.Name = "Brawling Stance";
		this.Description = "Give a minion +2/+2 and 50% chance to attack the wrong enemy.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		target.AddAttackModifier(new Func<int, int>(this.BrawlingStanceModifier));
		target.CurrentHealth += 2;
		target.AddHealthModifier(new Func<int, int>(this.BrawlingStanceModifier));
		target.IsInaccurate = true;
		yield break;
	}

	public int BrawlingStanceModifier(int value)
	{
		return value + 2;
	}
}
