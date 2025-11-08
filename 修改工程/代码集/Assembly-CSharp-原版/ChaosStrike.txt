using System;
using System.Collections;

public class ChaosStrike : SpellCard
{
	public ChaosStrike()
	{
		this.Name = "Chaos Strike";
		this.Description = "Deal 3 damage. If the target's Attack is odd, deal 5 damage instead.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (target.CurrentAttack % 2 == 0)
		{
			yield return target.Damage(null, 3 + this.Player.GetSpellPower());
		}
		else
		{
			yield return target.Damage(null, 5 + this.Player.GetSpellPower());
		}
		yield return target.CheckDeath();
		yield break;
	}
}
