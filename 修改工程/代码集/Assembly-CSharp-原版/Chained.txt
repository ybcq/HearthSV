using System;

public class Chained : SpellCard
{
	public Chained()
	{
		this.Name = "Chained";
		this.Description = "Held: Your minions cost (2) more.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		this.HasHeld = true;
		this.CardAura = new Aura<BaseCard>(new Action<BaseCard>(this.ApplyAura), new Action<BaseCard>(this.RemoveAura), new Func<BaseCard, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeSpell();
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
		return baseCard.Player == this.Player && baseCard is MinionCard;
	}

	public bool ExistCondition()
	{
		return this.Player.Hand.Contains(this);
	}
}
