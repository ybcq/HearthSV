using System;
using System.Collections;

public class FlamingPumpkin : MinionCard
{
	public FlamingPumpkin()
	{
		this.Name = "Flaming Pumpkin";
		this.Description = "While this is in your hand, take 1 damage at the end of your turn.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.Collectible = false;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Mechanics.OnHandTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnHandTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnHandTurnEnd(TurnEvent turnEvent)
	{
		if (turnEvent.Player == this.Player)
		{
			yield return this.Player.Hero.Damage(null, 1);
			yield return this.Player.Hero.CheckDeath();
		}
		yield break;
	}
}
