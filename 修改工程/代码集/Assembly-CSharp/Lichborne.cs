using System;
using System.Collections;

public class Lichborne : SpellCard
{
	public Lichborne()
	{
		this.Name = "巫妖的诱惑";
		this.Description = "Give a friendly minion Taunt and change its Type to Undead.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		minion.HasTaunt = true;
		minion.Card.MinionType = MinionType.Undead;
		yield break;
	}
}
