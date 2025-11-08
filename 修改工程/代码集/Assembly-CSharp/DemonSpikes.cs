using System;
using System.Collections;

public class DemonSpikes : SpellCard
{
	public DemonSpikes()
	{
		this.Name = "死亡捷径";
		this.Description = "Give a minion Poison.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		((Minion)target).Poison();
		yield break;
	}
}
