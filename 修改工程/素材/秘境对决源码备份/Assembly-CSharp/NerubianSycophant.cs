using System;
using System.Collections;
using UnityEngine;

public class NerubianSycophant : MinionCard
{
	public NerubianSycophant()
	{
		this.Name = "纳鲁比·西科芬";
		this.Description = "Inspire: Summon a 1/1 Ghoul with Charge.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.Mechanics.OnInspired.Add(new Func<InspireEvent, IEnumerator>(this.OnInspired));
		base.InitializeMinion();
	}

	public IEnumerator OnInspired(InspireEvent evt)
	{
		if (evt.Hero.Player == this.Player)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			ChargeTurnGhoul NerubianSycophantCard = new ChargeTurnGhoul();
			yield return this.Player.SummonMinion(NerubianSycophantCard);
			if (NerubianSycophantCard.Minion != null)
			{
				NerubianSycophantCard.Minion.HasCharge = false;
				NerubianSycophantCard.Minion.Mechanics.RemoveAll();
			}
			NerubianSycophantCard = null;
			NerubianSycophantCard = null;
			NerubianSycophantCard = null;
		}
		yield break;
	}
}
