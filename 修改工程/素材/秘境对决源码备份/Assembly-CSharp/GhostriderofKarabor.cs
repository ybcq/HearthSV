using System;

public class GhostriderofKarabor : MinionCard
{
	public GhostriderofKarabor()
	{
		this.Name = "亡灵法师";
		this.Description = "Spell Damage -2. Your spells cost (3) less.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
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
