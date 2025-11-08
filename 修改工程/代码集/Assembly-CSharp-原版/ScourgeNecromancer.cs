using System;
using System.Collections;

public class ScourgeNecromancer : MinionCard
{
	public ScourgeNecromancer()
	{
		this.Name = "Scourge Necromancer";
		this.Description = "At the end of your turn, if a minion died this turn, summon a 1/1 Ghoul.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 4;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		if (turnEvent.Player == this.Player && GameManager.Instance.CurrentTurnDeadMinions > 0)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return this.Player.SummonMinion(new NormalGhoul());
		}
		yield break;
	}
}
