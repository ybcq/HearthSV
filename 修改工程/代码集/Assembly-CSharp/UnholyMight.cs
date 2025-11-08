using System;
using System.Collections;

public class UnholyMight : SpellCard
{
	public UnholyMight()
	{
		this.Name = "邪恶之力";
		this.Description = "Choose a minion. Adjacent minions have its Attack.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		foreach (Minion minion in targetMinion.Player.Minions)
		{
			if (minion.IsNextTo(targetMinion))
			{
				minion.AddAttackModifier((int x) => targetMinion.CurrentAttack);
			}
		}
		yield break;
	}
}
