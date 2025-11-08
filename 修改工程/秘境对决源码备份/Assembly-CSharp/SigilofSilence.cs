using System;
using System.Collections;

public class SigilofSilence : SpellCard
{
	public SigilofSilence()
	{
		this.Name = "破邪圣光";
		this.Description = "Remove an enemy minion.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int healmun = target.CurrentHealth;
		target.As<Minion>().Mechanics.RemoveAll();
		yield return target.As<Minion>().Destroy();
		yield return target.CheckDeath();
		yield return this.Player.Hero.Heal(healmun);
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}
}
