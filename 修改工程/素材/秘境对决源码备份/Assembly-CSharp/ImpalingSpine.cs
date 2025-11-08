using System;
using System.Collections;
using System.Linq;

public class ImpalingSpine : SpellCard
{
	public ImpalingSpine()
	{
		this.Name = "神圣打击";
		this.Description = "驱散目标生物，然后对它造成2点伤害。";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().Silence();
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 2 + this.Player.GetSpellPower());
		yield return target.Damage(null, 2 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield break;
	}

	public override bool CanTarget(Character target)
	{
		return target != null && !target.HasSpellshield && (target.IsFriendlyOf(this.Player.Hero) || !target.IsStealth) && target.IsMinion() && target.As<Minion>().Card.MinionType == MinionType.Biol;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Any((Minion m) => m.Card.MinionType == MinionType.Biol);
	}
}
