using System;

public class ElwynnForestWolf : MinionCard
{
	public ElwynnForestWolf()
	{
		this.Name = "被冰封的勇士";
		this.Description = "Permanently Frozen. Adjacent minions are Immune to Frost Breath.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 2;
		this.BaseHealth = 10;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		this.Minion.IsFrozen = true;
		minion.IsFrozen = false;
	}

	public void RemoveAura(Minion minion)
	{
		minion.IsFrozen = minion.IsFrozen;
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
