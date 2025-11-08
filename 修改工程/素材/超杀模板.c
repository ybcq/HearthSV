using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CustodianofLife : MinionCard
{
	public CustodianofLife()
	{
		this.Name = "Ò°»ðÔªËØ";
		this.Description = "After this attacks and kills a minion, deal excess damage to a random adjacent minion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 6;
		this.BaseAttack = 7;
		this.BaseHealth = 3;
		this.Mechanics.OnMinionPreDamage.Add(new Func<MinionPreDamageEvent, IEnumerator>(this.OnMinionPreDamage));
		this.Mechanics.OnMinionDamaged.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnMinionDamaged));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionPreDamage(MinionPreDamageEvent evt)
	{
		if (evt.Minion.IsEnemyOf(this.Player.Hero) && evt.Minion.CurrentHealth < evt.Attacker.CurrentAttack)
		{
			this.HasChaoShaNum = evt.Attacker.CurrentAttack - evt.Minion.CurrentHealth;
			this.HasChaoSha = true;
		}
		yield break;
	}

	public IEnumerator OnMinionDamaged(MinionDamagedEvent evt)
	{
		if (evt.Minion.IsEnemyOf(this.Player.Hero) && this.HasChaoSha)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			Minion randomMinion = RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
			where m.IsAlive()
			select m).ToList<Minion>());
			if (randomMinion != null)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(this.Minion.Controller, this.HasChaoShaNum);
				yield return randomMinion.Damage(this.Minion, this.HasChaoShaNum);
			}
			yield return new WaitForSeconds(0.25f);
			yield break;
		}
		yield break;
	}

	public bool HasChaoSha;

	public int HasChaoShaNum;
}
