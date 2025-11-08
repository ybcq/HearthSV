using System;
using System.Collections;

public class SigilofMisery : SpellCard
{
	public SigilofMisery()
	{
		this.Name = "不洁重生";
		this.Description = "Destroy an friendly minion and summon a lich.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.As<Minion>().Destroy();
		yield return target.CheckDeath();
		yield return this.Player.SummonMinion(new ExhumedLich
		{
			BaseCost = 4,
			BaseHealth = 4,
			BaseAttack = 4,
			CurrentHealth = 4
		});
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}
}
