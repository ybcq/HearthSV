using System;
using System.Collections;

public class FrostFever : SpellCard
{
	public FrostFever()
	{
		this.Name = "冰霜热";
		this.Description = "Give a friendly minion \"Freeze any character damaged by this minion.\"";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().HasFreeze = true;
		yield break;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}
}
