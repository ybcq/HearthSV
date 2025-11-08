using System;
using System.Collections;

public class Necrosis : SpellCard
{
	public Necrosis()
	{
		this.Name = "Necrosis";
		this.Description = "Deal 4 damage. Costs (1) less for each minion that died this turn.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 4;
		base.AddCostModifier(new Func<int, int>(this.MinionDiedModifier));
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 4 + this.Player.GetSpellPower();
		yield return target.Damage(null, damage);
		yield return target.CheckDeath();
		yield break;
	}

	public int MinionDiedModifier(int cost)
	{
		return cost - GameManager.Instance.CurrentTurnDeadMinions;
	}
}
