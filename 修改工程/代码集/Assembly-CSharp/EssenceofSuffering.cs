using System;
using System.Collections;

public class EssenceofSuffering : SpellCard
{
	public EssenceofSuffering()
	{
		this.Name = "利爪的一击";
		this.Description = "Deal 2 damage to your Hero and Deal 3 damage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyCharacters;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 3 + this.Player.GetSpellPower());
		yield return this.Player.Hero.Damage(null, 2 + this.Player.GetSpellPower());
		yield return target.Damage(null, 3 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield break;
	}
}
