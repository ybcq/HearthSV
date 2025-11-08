using System;
using System.Collections;
using System.Linq;

public class TastyBrew : SpellCard
{
	public TastyBrew()
	{
		this.Name = "Tasty Brew";
		this.Description = "Give a minion +1/+1.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		this.Collectible = false;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		target.AddAttackModifier(new Func<int, int>(this.TastyBrewModifier));
		target.CurrentHealth++;
		target.AddHealthModifier(new Func<int, int>(this.TastyBrewModifier));
		yield break;
	}

	public int TastyBrewModifier(int value)
	{
		return value + 1;
	}
}
