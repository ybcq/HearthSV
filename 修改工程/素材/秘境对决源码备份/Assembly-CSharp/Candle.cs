using System;
using System.Collections;
using System.Linq;

public class Candle : SpellCard
{
	public Candle()
	{
		this.Name = "寒冰吐息";
		this.Description = "冻结目标生物。抓一张牌。";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().Freeze();
		yield return this.Player.Draw(null);
		yield break;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Any((Minion m) => m.Card.MinionType == MinionType.Biol);
	}

	public override bool CanTarget(Character target)
	{
		return target != null && (target.IsFriendlyOf(this.Player.Hero) || !target.IsStealth) && !target.HasSpellshield && target.IsMinion() && target.As<Minion>().Card.MinionType == MinionType.Biol;
	}
}
