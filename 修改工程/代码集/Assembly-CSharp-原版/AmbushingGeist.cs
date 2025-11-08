using System;
using System.Collections;
using UnityEngine;

public class AmbushingGeist : MinionCard
{
	public AmbushingGeist()
	{
		this.Name = "Ambushing Geist";
		this.Description = "Charge. Whenever an enemy minion dies, gain Stealth.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.HasCharge = true;
		this.Mechanics.OnMinionDied.Add(new Func<MinionDiedEvent, IEnumerator>(this.OnMinionDied));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionDied(MinionDiedEvent evt)
	{
		if (evt.Minion.IsEnemyOf(this.Minion))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Minion.IsStealth = true;
		}
		yield break;
	}
}
