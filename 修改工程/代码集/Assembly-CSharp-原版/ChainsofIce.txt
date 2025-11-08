using System;
using System.Collections;

public class ChainsofIce : SpellCard
{
	public ChainsofIce()
	{
		this.Name = "Chains of Ice";
		this.Description = "Freeze an enemy minion and give it \"Your minions cost (2) more.\"";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		this.Target = (Minion)target;
		this.Target.Freeze();
		AuraManager.Instance.AddCardAura(new Aura<BaseCard>(new Action<BaseCard>(this.ApplyAura), new Action<BaseCard>(this.RemoveAura), new Func<BaseCard, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition)));
		yield break;
	}

	public void ApplyAura(BaseCard baseCard)
	{
		baseCard.AddCostModifier(new Func<int, int>(this.ApplyCostModifier));
	}

	public void RemoveAura(BaseCard baseCard)
	{
		baseCard.RemoveCostModifier(new Func<int, int>(this.ApplyCostModifier));
	}

	public int ApplyCostModifier(int cost)
	{
		return cost + 2;
	}

	public bool ApplyCondition(BaseCard baseCard)
	{
		return baseCard.Player == this.Target.Player && baseCard is MinionCard;
	}

	public bool ExistCondition()
	{
		return this.Target != null && GameManager.Instance.GetAllMinions().Contains(this.Target) && this.Target.IsAlive();
	}

	public Minion Target;
}
