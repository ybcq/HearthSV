using System;

public class CustodianofLife : MinionCard
{
	public CustodianofLife()
	{
		this.Name = "生命保管人";
		this.Description = "Your other minions have +3 Health.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 7;
		this.BaseAttack = 4;
		this.BaseHealth = 8;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.AddAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier), 3);
	}

	public void RemoveAura(Minion minion)
	{
		minion.RemoveAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
	}

	public int ApplyHealthModifier(int health)
	{
		return health + 3;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion.Player == this.Player && minion != this.Minion;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
