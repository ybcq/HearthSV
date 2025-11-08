using System;

public class ScaleswornSkyrazor : MinionCard
{
	public ScaleswornSkyrazor()
	{
		this.Name = "鳞片破天剃刀";
		this.Description = "Spell Damage -2. Your spells cost (3) less.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 7;
		this.BaseAttack = 3;
		this.BaseHealth = 9;
		this.SpellPower = -2;
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
		return cost - 3;
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
