using System;
using System.Collections;

public class ChargeGhoul : MinionCard
{
	public ChargeGhoul()
	{
		this.Name = "侍僧";
		this.Description = "Whenever another minion gains or loses Attack, gain or lose as much.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 3;
		this.Mechanics.OnMinionBuffAttack.Add(new Func<MinionBuffEvent, IEnumerator>(this.OnMinionBuffAttack));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionBuffAttack(MinionBuffEvent evt)
	{
		if (evt.Minion.Card != this.Minion.Card)
		{
			this.Minion.AddAttackModifier((int a) => a + evt.Delta);
		}
		yield break;
	}
}
