using System;
using System.Collections;

public class FrostmaneTroll : MinionCard
{
	public FrostmaneTroll()
	{
		this.Name = "Frostmane Troll";
		this.Description = "At the start of your turn, restore this minion to full Health.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent turnEvent)
	{
		if (this.Player == turnEvent.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return this.Minion.Heal(this.Minion.GetMissingHealth());
			yield return this.Minion.CheckDeath();
		}
		yield break;
	}
}
