using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GiftoftheSerpent : SpellCard
{
	public GiftoftheSerpent()
	{
		this.Name = "Gift of the Serpent";
		this.Description = "Destroy a minion with equal mana cost to one of your minions.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Any(new Func<Minion, bool>(this.CanTarget));
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.As<Minion>().Destroy();
		yield break;
	}

	public override bool CanTarget(Character target)
	{
		if (target != null && (target.IsFriendlyOf(this.Player.Hero) || (!target.IsStealth && !target.HasSpellshield)) && target.IsMinion())
		{
			List<int> list = (from m in this.Player.Minions
			select m.Card.BaseCost).ToList<int>();
			if (list.Contains(target.As<Minion>().Card.BaseCost))
			{
				return true;
			}
		}
		return false;
	}
}
