using System;
using System.Collections;

public class DemonSpikes : SpellCard
{
	public DemonSpikes()
	{
		this.Name = "Demon Spikes";
		this.Description = "Give a minion Poison and change its type to Demon.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		minion.Poison();
		minion.Card.MinionType = MinionType.Demon;
		yield break;
	}
}
