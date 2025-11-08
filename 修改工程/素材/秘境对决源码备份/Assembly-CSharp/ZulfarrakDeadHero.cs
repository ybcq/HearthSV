using System;

public class ZulfarrakDeadHero : MinionCard
{
	public ZulfarrakDeadHero()
	{
		this.Name = "诡雷实验员";
		this.Description = "You gain +1 power for each artifact you control.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 4;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		if (this.Player.HasWeapon())
		{
			minion.AddAuraAttackModifier(new Func<int, int>(this.ApplyHealthModifier));
		}
	}

	public void RemoveAura(Minion minion)
	{
		minion.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyHealthModifier));
	}

	public int ApplyHealthModifier(int health)
	{
		return health + 1;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion == this.Minion;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
