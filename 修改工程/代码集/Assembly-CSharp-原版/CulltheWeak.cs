using System;
using System.Collections;

public class CulltheWeak : SpellCard
{
	public CulltheWeak()
	{
		this.Name = "Cull the Weak";
		this.Description = "Destroy all damaged minions with 5 or less Attack.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion.IsDamaged() && minion.CurrentAttack <= 5)
			{
				yield return minion.Destroy();
			}
		}
		yield break;
	}
}
