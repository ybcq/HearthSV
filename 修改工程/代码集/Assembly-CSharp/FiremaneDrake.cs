using System;

public class FiremaneDrake : MinionCard
{
	public FiremaneDrake()
	{
		this.Name = "火鬃龙";
		this.Description = "Non-Dragon minions have -2 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 7;
		this.BaseAttack = 8;
		this.BaseHealth = 6;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public void RemoveAura(Minion minion)
	{
		minion.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack - 2;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion.Card.MinionType != MinionType.Dragon;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
