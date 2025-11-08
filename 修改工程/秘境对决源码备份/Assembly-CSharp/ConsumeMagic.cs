using System;
using System.Collections;
using System.Linq;

public class ConsumeMagic : SpellCard
{
	public ConsumeMagic()
	{
		this.Name = "不灭的怨念";
		this.Description = "Inflicts 3 damage to an enemy's entourage. Necromancer 2; The original 2 damage is converted to 5 damage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.Player.DeadMinions.Count > 2)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 5 + this.Player.GetSpellPower());
			yield return target.Damage(null, 5 + this.Player.GetSpellPower());
			yield return target.CheckDeath();
		}
		else
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 3 + this.Player.GetSpellPower());
			yield return target.Damage(null, 3 + this.Player.GetSpellPower());
			yield return target.CheckDeath();
		}
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Any<Minion>();
	}
}
