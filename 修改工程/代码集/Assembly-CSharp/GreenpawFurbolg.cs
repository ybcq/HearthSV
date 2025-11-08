using System;
using System.Collections;

public class GreenpawFurbolg : MinionCard
{
	public GreenpawFurbolg()
	{
		this.Name = "血受";
		this.Description = "At the start of your turn, Restore your Hero 3 health.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			yield return this.Player.Hero.Heal(3);
			yield return this.Player.Hero.CheckDeath();
		}
		yield break;
	}
}
