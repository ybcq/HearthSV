using System;
using System.Collections;
using System.Linq;

public class EyeBeam : SpellCard
{
	public EyeBeam()
	{
		this.Name = "狂野追击";
		this.Description = "Destroy a damaged enemy minion.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.As<Minion>().Destroy();
		yield return target.CheckDeath();
		yield break;
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Any((Minion m) => m.IsDamaged());
	}

	public override bool CanTarget(Character target)
	{
		return target != null && target.IsMinion() && target.IsFriendlyOf(this.Player.Enemy.Hero) && !target.IsStealth && !target.HasSpellshield && target.IsDamaged();
	}
}
