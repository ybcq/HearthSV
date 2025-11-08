using System;
using System.Collections;

public class HeWhoActs : MinionCard
{
	public HeWhoActs()
	{
		this.Name = "黑龙骑士·法露特";
		this.Description = "Charge. if you are powerful, it become Immune";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 1;
		this.HasCharge = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			if (this.Player.TurnMana > 8)
			{
				yield return this.IsImmune = true;
			}
			yield break;
		}
		yield break;
	}

	public IEnumerator Battlecry(Character target)
	{
		if (this.Player.TurnMana == 10)
		{
			yield return this.IsImmune = true;
		}
		yield break;
	}
}
