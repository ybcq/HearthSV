using System;
using System.Collections;

public class Invincible : MinionCard
{
	public Invincible()
	{
		this.Name = "ÎÞµÐÕß";
		this.Description = "Deathrattle: The next Undead you play gains Charge.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		this.OnMinionPlayedSubscription = EventManager.Instance.MinionPlayedHandler.Add((MinionPlayedEvent x) => this.OnMinionPlayed(x, self));
		yield break;
	}

	public IEnumerator OnMinionPlayed(MinionPlayedEvent evt, Minion minion)
	{
		if (evt.Minion.IsFriendlyOf(minion) && evt.Minion.Card.MinionType == MinionType.Undead)
		{
			evt.Minion.HasCharge = true;
			this.OnMinionPlayedSubscription.Dispose();
		}
		yield break;
	}

	public IDisposable OnMinionPlayedSubscription;
}