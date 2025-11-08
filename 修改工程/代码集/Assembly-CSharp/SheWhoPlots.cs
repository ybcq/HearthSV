using System;

public class SheWhoPlots : MinionCard
{
	public SheWhoPlots()
	{
		this.Name = "She Who Plots";
		this.Description = "Evasion. Stealth. Your opponent's Held cards cost (2) more.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 3;
		this.IsEvasive = true;
		this.IsStealth = true;
		this.CardAura = new Aura<BaseCard>(new Action<BaseCard>(this.ApplyCardAura), new Action<BaseCard>(this.RemoveCardAura), new Func<BaseCard, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyCardAura(BaseCard card)
	{
		card.AddCostModifier(new Func<int, int>(this.SheWhoPlotsCostModifier));
	}

	public void RemoveCardAura(BaseCard card)
	{
		card.RemoveCostModifier(new Func<int, int>(this.SheWhoPlotsCostModifier));
	}

	public int SheWhoPlotsCostModifier(int cost)
	{
		return cost + 2;
	}

	public bool ApplyCondition(BaseCard card)
	{
		return card.Player == this.Player.Enemy && card.HasHeld;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
