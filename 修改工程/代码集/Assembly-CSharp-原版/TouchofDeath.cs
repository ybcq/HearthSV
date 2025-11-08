using System;
using System.Collections;

public class TouchofDeath : SpellCard
{
	public TouchofDeath()
	{
		this.Name = "Touch of Death";
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
		yield return player.Enemy.Hero.Damage(null, 7);
		yield return player.Enemy.Hero.CheckDeath();
		yield break;
	}
}
