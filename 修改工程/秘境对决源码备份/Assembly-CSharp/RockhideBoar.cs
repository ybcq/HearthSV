using System;
using System.Collections;
using UnityEngine;

public class RockhideBoar : MinionCard
{
	public RockhideBoar()
	{
		this.Name = "暗光源灵";
		this.Description = "Concealed. At the beginning of each round, each player puts the top card of the library into the graveyard.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.IsStealth = true;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.25f);
		yield return evt.Player.Draw(new Func<BaseCard, IEnumerator>(this.GrandInquisitorDraw));
		yield break;
	}

	public IEnumerator GrandInquisitorDraw(BaseCard card)
	{
		yield return card.Discard();
		yield break;
	}
}
