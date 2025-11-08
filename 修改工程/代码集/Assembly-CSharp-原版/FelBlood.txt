using System;
using System.Collections;
using System.Linq;

public class FelBlood : SpellCard
{
	public FelBlood()
	{
		this.Name = "Fel Blood";
		this.Description = "If you have a damaged Demon, draw a card and reduce the Cost of your Hero Power by (1).";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.Player.Minions.Any((Minion c) => c.IsDamaged() && c.Card.MinionType == MinionType.Demon))
		{
			yield return this.Player.Draw(null);
			this.Player.Hero.HeroPower.AddCostModifier(new Func<int, int>(this.FelBloodModifier));
		}
		yield break;
	}

	public int FelBloodModifier(int cost)
	{
		return cost - 1;
	}
}
