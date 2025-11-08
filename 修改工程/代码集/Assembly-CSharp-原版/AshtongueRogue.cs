using System;
using System.Collections;
using UnityEngine;

public class AshtongueRogue : MinionCard
{
	public AshtongueRogue()
	{
		this.Name = "Ashtongue Rogue";
		this.Description = "Whenever another minion gains or loses Attack, gain or lose as much.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 6;
		this.Mechanics.OnMinionBuffAttack.Add(new Func<MinionBuffEvent, IEnumerator>(this.OnMinionBuffAttack));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionBuffAttack(MinionBuffEvent evt)
	{
		if (!(evt.Minion.Card is AshtongueRogue))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Minion.AddAttackModifier((int a) => a + evt.Delta);
		}
		yield break;
	}
}
