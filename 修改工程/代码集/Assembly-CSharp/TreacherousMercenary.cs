using System;
using System.Collections;
using UnityEngine;

public class TreacherousMercenary : MinionCard
{
	public TreacherousMercenary()
	{
		this.Name = "若安，光辉圣女";
		this.Description = "Charge. 每当此生物攻击时，召唤一个1/1士兵。";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 6;
		this.BaseAttack = 4;
		this.BaseHealth = 5;
		this.HasCharge = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.OnMinionAttacked.Add(new Func<MinionAttackedEvent, IEnumerator>(this.OnMinionAttacked));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionAttacked(MinionAttackedEvent evt)
	{
		if (evt.Minion == this.Minion)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.5f);
			this.Minion.Player.SummonMinion(new TreacherousMercenary
			{
				BaseAttack = 1,
				BaseHealth = 1,
				HasCharge = true
			});
		}
		yield break;
	}
}
