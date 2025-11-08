using System;
using System.Collections;

public class SneakySapper : MinionCard
{
	public SneakySapper()
	{
		this.Name = "Sneaky Sapper";
		this.Description = "Stealth. Deathrattle: Deal 2 damage to the enemy hero.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.IsStealth = true;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.Enemy.Hero.Damage(null, 2);
		yield return self.Player.Enemy.Hero.CheckDeath();
		yield break;
	}
}
