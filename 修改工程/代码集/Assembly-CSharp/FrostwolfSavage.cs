using System;
using System.Collections;

public class FrostwolfSavage : MinionCard
{
	public FrostwolfSavage()
	{
		this.Name = "托维尔重甲兵";
		this.Description = "Deathrattle: Deal 5 damage to both heroes.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 5;
		this.BaseHealth = 2;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 5);
		yield return this.Player.Enemy.Hero.Damage(null, 5);
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Hero.Controller, 5);
		yield return this.Player.Hero.Damage(null, 5);
		yield return this.Player.Enemy.Hero.CheckDeath();
		yield return this.Player.Hero.CheckDeath();
		yield break;
	}
}
