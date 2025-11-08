using System;
using System.Collections;

public class RuneStrike : SpellCard
{
	public RuneStrike()
	{
		this.Name = "符文打击";
		this.Description = "Give an enemy -4 Health.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.EnemyCharacters;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 4);
		target.CurrentHealth -= 4;
		target.AddHealthModifier(new Func<int, int>(this.RuneStrikeModifier));
		yield return target.CheckDeath();
		yield break;
	}

	public int RuneStrikeModifier(int health)
	{
		return health - 4;
	}
}
