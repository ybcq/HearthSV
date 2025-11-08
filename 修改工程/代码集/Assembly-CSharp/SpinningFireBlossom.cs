using System;
using System.Collections;

public class SpinningFireBlossom : SpellCard
{
	public SpinningFireBlossom()
	{
		this.Name = "纺火花";
		this.Description = "Deal 3 damage. If your hero attacked this turn, deal 5 damage instead.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.Player.Hero.CurrentTurnAttacks == 0)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 3 + this.Player.GetSpellPower());
			yield return target.Damage(null, 3 + this.Player.GetSpellPower());
			yield return target.CheckDeath();
		}
		else
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 5 + this.Player.GetSpellPower());
			yield return target.Damage(null, 5 + this.Player.GetSpellPower());
			yield return target.CheckDeath();
		}
		yield break;
	}
}
