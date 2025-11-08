using System;
using System.Collections;

public class DemonBite : SpellCard
{
	public DemonBite()
	{
		this.Name = "巨像炼成术";
		this.Description = "Summon a Mud Colossus.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		HighWarlordNajentus DemonBiteCard = new HighWarlordNajentus
		{
			BaseCost = 2,
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		};
		yield return this.Player.SummonMinion(DemonBiteCard);
		if (DemonBiteCard.Minion != null)
		{
			DemonBiteCard.Minion.Mechanics.RemoveAll();
		}
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count < 7;
	}
}
