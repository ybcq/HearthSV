using System;
using System.Collections;
using UnityEngine;

public class DancingRuneblade : MinionCard
{
	public DancingRuneblade()
	{
		this.Name = "冰霜巨龙";
		this.Description = "Your opponent's characters cannot be healed and charge.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		this.Mechanics.OnCharacterPreHeal.Add(new Func<CharacterPreHealEvent, IEnumerator>(this.OnCharacterPreHeal));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.HasCharge = false;
	}

	public void RemoveAura(Minion minion)
	{
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion.Player == this.Player.Enemy;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
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
