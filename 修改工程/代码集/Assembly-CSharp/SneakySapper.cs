using System;
using System.Collections;

public class SneakySapper : MinionCard
{
	public SneakySapper()
	{
		this.Name = "砰砰箱";
		this.Description = "Stealth. Deathrattle: Deal 100 damage to the enemy hero.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 10;
		this.BaseAttack = 0;
		this.BaseHealth = 10;
		this.IsStealth = true;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 100);
		yield return self.Player.Enemy.Hero.Damage(null, 100);
		yield return self.Player.Enemy.Hero.CheckDeath();
		yield break;
	}
}
