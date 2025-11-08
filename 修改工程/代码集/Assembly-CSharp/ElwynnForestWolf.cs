using System;

public class ElwynnForestWolf : MinionCard
{
	public ElwynnForestWolf()
	{
		this.Name = "恐狼先锋";
		this.Description = "相邻的随从获得+2攻击力。";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.Golden = true;
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
		return attack + 2;
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
