using System;

public class ChampionoftheLight : MinionCard
{
	public ChampionoftheLight()
	{
		this.Name = "光之冠军";
		this.Description = "Divine Shield. Your cards cost (2) more.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 7;
		this.BaseAttack = 8;
		this.BaseHealth = 8;
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
		return cost + 2;
	}

	public bool ApplyCondition(BaseCard baseCard)
	{
		return baseCard.Player == this.Player;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
