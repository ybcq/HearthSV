using System;
using System.Collections;
using System.Linq;

public class ChaosStrike : SpellCard
{
	public ChaosStrike()
	{
		this.Name = "沸腾之血";
		this.Description = "Deal 3 damage to an Enemy Minion. If your Health is less than 10, deal 6 damage instead.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.Player.Hero.CurrentHealth <= 10)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 6 + this.Player.GetSpellPower());
			yield return target.Damage(null, 6 + this.Player.GetSpellPower());
		}
		else
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 3 + this.Player.GetSpellPower());
			yield return target.Damage(null, 3 + this.Player.GetSpellPower());
		}
		yield return target.CheckDeath();
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Any<Minion>();
	}
}
