using System;
using System.Collections;
using UnityEngine;

public class Bloodworm : MinionCard
{
	public Bloodworm()
	{
		this.Name = "血虫";
		this.Description = "Deathrattle: Restore 2 Health to your hero.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 0;
		this.BaseAttack = 0;
		this.BaseHealth = 1;
		this.Collectible = false;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.25f);
		yield return self.Player.Hero.Heal(2);
		yield break;
	}
}
