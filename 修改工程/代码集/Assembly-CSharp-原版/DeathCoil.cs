using System;
using System.Collections;
using System.Linq;

public class DeathCoil : SpellCard
{
	public DeathCoil()
	{
		this.Name = "Death Coil";
		this.Description = "Deal 2 damage to a minion. If it's a friendly Undead, restore it to full Health instead.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		if (target.IsFriendlyOf(this.Player.Hero) && target.As<Minion>().Card.MinionType == MinionType.Undead)
		{
			yield return target.Heal(target.GetMissingHealth());
			yield return target.CheckDeath();
		}
		else
		{
			int damage = 2 + this.Player.GetSpellPower();
			yield return target.Damage(null, damage);
			yield return target.CheckDeath();
		}
		yield break;
	}
}
