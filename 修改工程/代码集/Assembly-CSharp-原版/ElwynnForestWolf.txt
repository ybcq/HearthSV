using System;

public class ElwynnForestWolf : MinionCard
{
	public ElwynnForestWolf()
	{
		this.Name = "Elwynn Forest Wolf";
		this.Description = "Adjacent minions have +1 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
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
		return attack + 1;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion != this.Minion && minion.IsFriendlyOf(this.Minion) && minion.IsNextTo(this.Minion);
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
