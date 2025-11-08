using System;

public class DragonmawWrangler : MinionCard
{
	public DragonmawWrangler()
	{
		this.Name = "Dragonmaw Wrangler";
		this.Description = "Your dragons cost (2) less but have -1/-1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseHealth = 6;
		this.CardAura = new Aura<BaseCard>(new Action<BaseCard>(this.ApplyCardAura), new Action<BaseCard>(this.RemoveCardAura), new Func<BaseCard, bool>(this.ApplyCardCondition), new Func<bool>(this.ExistCardCondition));
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyMinionAura), new Action<Minion>(this.RemoveMinionAura), new Func<Minion, bool>(this.ApplyMinionCondition), new Func<bool>(this.ExistMinionCondition));
		base.InitializeMinion();
	}

	public void ApplyCardAura(BaseCard baseCard)
	{
		baseCard.AddCostModifier(new Func<int, int>(this.ApplyCardCostModifier));
	}

	public void RemoveCardAura(BaseCard baseCard)
	{
		baseCard.RemoveCostModifier(new Func<int, int>(this.ApplyCardCostModifier));
	}

	public int ApplyCardCostModifier(int cost)
	{
		return cost - 2;
	}

	public bool ApplyCardCondition(BaseCard baseCard)
	{
		return baseCard.Player == this.Player && baseCard is MinionCard && (baseCard as MinionCard).MinionType == MinionType.Dragon;
	}

	public bool ExistCardCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}

	public void ApplyMinionAura(Minion baseCard)
	{
		baseCard.AddAuraAttackModifier(new Func<int, int>(this.ApplyMinionModifier));
		baseCard.AddAuraHealthModifier(new Func<int, int>(this.ApplyMinionModifier));
	}

	public void RemoveMinionAura(Minion baseCard)
	{
		baseCard.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyMinionModifier));
		baseCard.RemoveAuraHealthModifier(new Func<int, int>(this.ApplyMinionModifier));
	}

	public int ApplyMinionModifier(int value)
	{
		return value - 1;
	}

	public bool ApplyMinionCondition(Minion minion)
	{
		return minion.Player == this.Player && minion.Card.MinionType == MinionType.Dragon;
	}

	public bool ExistMinionCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
