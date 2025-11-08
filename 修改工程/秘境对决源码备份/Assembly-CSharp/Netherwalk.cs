using System;
using System.Collections;

public class Netherwalk : SpellCard
{
	public Netherwalk()
	{
		this.Name = "魔导飞弹";
		this.Description = "Deal 1 damage to an enemy. Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyCharacters;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 1 + this.Player.GetSpellPower());
		yield return target.Damage(null, 1 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield return this.Player.Draw(1, null);
		yield break;
	}
}
