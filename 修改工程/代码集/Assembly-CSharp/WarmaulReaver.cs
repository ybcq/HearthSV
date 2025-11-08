using System;
using System.Collections;
using UnityEngine;

public class WarmaulReaver : MinionCard
{
	public WarmaulReaver()
	{
		this.Name = "温暖掠夺者";
		this.Description = "Your opponent's characters cannot be healed.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.Mechanics.OnCharacterPreHeal.Add(new Func<CharacterPreHealEvent, IEnumerator>(this.OnCharacterPreHeal));
		base.InitializeMinion();
	}

	public IEnumerator OnCharacterPreHeal(CharacterPreHealEvent evt)
	{
		if (evt.Character.IsEnemyOf(this.Minion))
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			evt.Cancel();
			yield return new WaitForSeconds(0.5f);
		}
		yield break;
	}
}
