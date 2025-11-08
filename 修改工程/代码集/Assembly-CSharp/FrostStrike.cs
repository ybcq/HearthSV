using System;
using System.Collections;

public class FrostStrike : SpellCard
{
	public FrostStrike()
	{
		this.Name = "冰霜打击";
		this.Description = "Deal 3 damage. This spell deals double damage to Frozen targets.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int num = 3 + this.Player.GetSpellPower();
		if (target.IsFrozen)
		{
			num *= 2;
		}
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, num);
		yield return target.Damage(null, num);
		yield return target.CheckDeath();
		yield break;
	}
}
