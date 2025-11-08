using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeilShadowrunner : MinionCard
{
	public VeilShadowrunner()
	{
		this.Name = "±»ÕÛÄ¥µÄ¼ÀÊ¦";
		this.Description = "Taunt Whenever this is attacked, give adjacent minions +1/+1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.HasTaunt = true;
		this.Mechanics.OnPreDamage.Add(new Func<MinionPreDamageEvent, IEnumerator>(this.OnPreDamage));
	}

	public IEnumerator OnPreDamage(MinionPreDamageEvent evt)
	{
		if (evt.Minion == this.Minion)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			using (List<Minion>.Enumerator enumerator = this.Player.Minions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Minion minion = enumerator.Current;
					if (minion.IsNextTo(this.Minion))
					{
						minion.AddAuraAttackModifier(new Func<int, int>(this.VeilShadowrunnerModifier));
						minion.CurrentHealth++;
						minion.AddAuraHealthModifier(new Func<int, int>(this.VeilShadowrunnerModifier));
					}
				}
				yield break;
			}
		}
		yield break;
	}

	public int VeilShadowrunnerModifier(int value)
	{
		return value + 1;
	}
}
