using System;
using System.Collections;
using System.Linq;

public class DemonSpikes : SpellCard
{
	public DemonSpikes()
	{
		this.Name = "死亡捷径";
		this.Description = "Give 1 of your minion Poison.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		((Minion)target).Poison();
		yield break;
	}
}
