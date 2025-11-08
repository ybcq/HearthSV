using System;
using System.Collections;
using System.Linq;

public class DeathStrike : SpellCard
{
	public DeathStrike()
	{
		this.Name = "致死打击";
		this.Description = "Destroy a Deathrattle minion. Restore 3 Health to your hero.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Any((Minion m) => m.Mechanics.HasDeathrattle());
	}

	public override bool CanTarget(Character target)
	{
		return target != null && (target.IsFriendlyOf(this.Player.Hero) || !target.IsStealth) && !target.HasSpellshield && target.IsMinion() && target.As<Minion>().Mechanics.HasDeathrattle();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.As<Minion>().Destroy();
		yield return this.Player.Hero.Heal(3);
		yield break;
	}
}
