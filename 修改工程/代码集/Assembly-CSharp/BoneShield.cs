using System;
using System.Collections;

public class BoneShield : SpellCard
{
	public BoneShield()
	{
		this.Name = "骨盾";
		this.Description = "Gain 5 Armor. Draw a card. Costs (1) less for each minion that died this turn.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 5;
		base.AddCostModifier(new Func<int, int>(this.MinionDiedModifier));
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Hero.CurrentArmor += 5;
		yield return this.Player.Draw(null);
		yield break;
	}

	public int MinionDiedModifier(int cost)
	{
		return cost - GameManager.Instance.CurrentTurnDeadMinions;
	}
}
