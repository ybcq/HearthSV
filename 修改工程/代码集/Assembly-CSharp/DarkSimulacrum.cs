using System;
using System.Collections;
using System.Linq;

public class DarkSimulacrum : SpellCard
{
	public DarkSimulacrum()
	{
		this.Name = "黑暗模拟";
		this.Description = "Put a copy of the last spell used by your opponent into your hand. It costs (3) less.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.PlayedSpells.Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		BaseCard lastSpell = this.Player.Enemy.PlayedSpells.Last<SpellCard>().Copy();
		lastSpell.AddCostModifier(new Func<int, int>(this.DarkSimulacrumModifier));
		yield return this.Player.AddCardToHand(lastSpell);
		yield break;
	}

	public int DarkSimulacrumModifier(int cost)
	{
		return cost - 3;
	}
}
