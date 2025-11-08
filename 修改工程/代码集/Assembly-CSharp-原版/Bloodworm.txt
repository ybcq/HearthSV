using System;
using System.Collections;

public class Bloodworm : MinionCard
{
	public Bloodworm()
	{
		this.Name = "Bloodworm";
		this.Description = "Deathrattle: Restore 2 Health to your hero.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.Collectible = false;
		this.BaseCost = 0;
		this.BaseAttack = 0;
		this.BaseHealth = 1;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.Hero.Heal(2);
		yield break;
	}
}
