using System;

public class DragonmawWrangler : MinionCard
{
	public DragonmawWrangler()
	{
		this.Name = "纳迦海巫";
		this.Description = "Your cards cost (5).";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.CardAura = new Aura<BaseCard>(new Action<BaseCard>(this.ApplyCardAura), new Action<BaseCard>(this.RemoveCardAura), new Func<BaseCard, bool>(this.ApplyCardCondition), new Func<bool>(this.ExistCardCondition));
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
		return 5;
	}

	public bool ApplyCardCondition(BaseCard baseCard)
	{
		return baseCard.Player == this.Player;
	}

	public bool ExistCardCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
