using System;
using System.Collections;
using System.Collections.Generic;

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
		using (List<Minion>.Enumerator enumerator = targetMinion.Player.Minions.GetEnumerator())
		{
			Func<int, int> <>9__0;
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				if (minion.IsNextTo(targetMinion) && minion.Card.MinionType != MinionType.Totem)
				{
					Character character = minion;
					Func<int, int> modifier;
					if ((modifier = <>9__0) == null)
					{
						modifier = (<>9__0 = ((int x) => targetMinion.CurrentAttack));
					}
					character.AddAttackModifier(modifier);
				}
			}
			yield break;
		}
		yield break;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Count > 0;
	}
}
