using System;

public class ZulfarrakDeadHero : MinionCard
{
	public ZulfarrakDeadHero()
	{
		this.Name = "Zul'farrak Dead Hero";
		this.Description = "Enemy minions have -1 Health.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 5;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.AddAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier), -1);
	}

	public void RemoveAura(Minion minion)
	{
		minion.RemoveAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
	}

	public int ApplyHealthModifier(int health)
	{
		return health - 1;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion.Player != this.Player;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
