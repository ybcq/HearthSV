using System;
using System.Collections;

public class FrostwolfSavage : MinionCard
{
	public FrostwolfSavage()
	{
		this.Name = "冷酷骑士";
		this.Description = "Deathrattle: Summon a Spectral Rider for your opponent.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 6;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return this.Player.Enemy.SummonMinion(new CrushridgeMauler());
		yield return this.Player.Hero.CheckDeath();
		yield break;
	}
}
