using System;

public class MadGrell : MinionCard
{
	public MadGrell()
	{
		this.Name = "侍女瓦丝琪";
		this.Description = "Spell damage +1. Your spells cost (1) less.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 2;
		this.SpellPower = 1;
		this.CardAura = new Aura<BaseCard>(new Action<BaseCard>(this.ApplyAura), new Action<BaseCard>(this.RemoveAura), new Func<BaseCard, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
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
		return cost - 1;
	}

	public bool ApplyCondition(BaseCard baseCard)
	{
		return baseCard.Player == this.Player && baseCard is SpellCard;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
