using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FieryBrand : SpellCard
{
	public FieryBrand()
	{
		this.Name = "Fiery Brand";
		this.Description = "Trigger a minion's Battlecry, then deal 1 damage to it.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override bool CanTarget(Character target)
	{
		return target != null && target.IsMinion() && (target.IsFriendlyOf(this.Player.Hero) || !target.IsStealth) && !target.HasSpellshield;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		if (targetMinion.Mechanics.HasBattlecry())
		{
			if (targetMinion.Card.BattlecryType == BattlecryType.NoTarget)
			{
				yield return targetMinion.Mechanics.Battlecry.Fire(null);
			}
			else
			{
				List<Character> possibleTargets = GameManager.Instance.GetAllCharacters().Where(new Func<Character, bool>(targetMinion.Card.CanBattlecryTarget)).ToList<Character>();
				Character randomCharacter = RNG.RandomItemFrom<Character>(possibleTargets);
				if (randomCharacter != null)
				{
					yield return targetMinion.Mechanics.Battlecry.Fire(randomCharacter);
				}
			}
		}
		yield return targetMinion.Damage(null, 1 + this.Player.GetSpellPower());
		yield return targetMinion.CheckDeath();
		yield break;
	}
}
