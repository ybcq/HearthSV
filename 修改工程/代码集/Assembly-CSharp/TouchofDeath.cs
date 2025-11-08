using System;
using System.Collections;

public class TouchofDeath : SpellCard
{
	public TouchofDeath()
	{
		this.Name = "灭亡之触";
		this.Description = "Meditate: Deal 7 damage to the enemy hero.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		this.Mechanics.Meditate.Add(new Func<Player, IEnumerator>(this.Meditate));
		base.InitializeSpell();
	}

	public IEnumerator Meditate(Player player)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 7 + this.Player.GetSpellPower());
		yield return player.Enemy.Hero.Damage(null, 7 + this.Player.GetSpellPower());
		yield return player.Enemy.Hero.CheckDeath();
		yield break;
	}
}
