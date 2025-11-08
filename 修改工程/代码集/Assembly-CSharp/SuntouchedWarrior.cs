using System;
using System.Collections;

public class SuntouchedWarrior : MinionCard
{
	public SuntouchedWarrior()
	{
		this.Name = "青铜守卫";
		this.Description = "Divine Shield, Rebirth";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 1;
		this.HasDivineShield = true;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.SummonMinion(new FireElemental());
		yield break;
	}
}
