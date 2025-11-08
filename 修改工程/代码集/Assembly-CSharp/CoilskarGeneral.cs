using System;

public class CoilskarGeneral : MinionCard
{
	public CoilskarGeneral()
	{
		this.Name = "Coilskar General";
		this.Description = "Adjacent Nagas have Cleave.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 7;
		this.BaseAttack = 8;
		this.BaseHealth = 7;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.HasCleave = true;
	}

	public void RemoveAura(Minion minion)
	{
		minion.HasCleave = false;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion != this.Minion && minion.IsFriendlyOf(this.Minion) && minion.IsNextTo(this.Minion) && minion.Card.MinionType == MinionType.Naga;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
