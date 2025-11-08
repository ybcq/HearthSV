using System;
using System.Collections;

public class VengefulSpirit : MinionCard
{
	public VengefulSpirit()
	{
		this.Name = "Vengeful Spirit";
		this.Description = "Deathrattle: Summon a 2/1 Spiteful Wraith.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 2;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.SummonMinion(new SpitefulWraith());
		yield break;
	}
}
