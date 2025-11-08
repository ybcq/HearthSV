using System;
using System.Collections;

public class AcherusDeathcharger : MinionCard
{
	public AcherusDeathcharger()
	{
		this.Name = "≥‡—Êƒœπœ";
		this.Description = "While this is in your hand, take 1 damage at the end of your turn.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.Collectible = false;
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
