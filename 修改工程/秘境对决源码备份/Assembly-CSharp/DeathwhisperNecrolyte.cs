using System;

public class DeathwhisperNecrolyte : MinionCard
{
	public DeathwhisperNecrolyte()
	{
		this.Name = "死亡低语者";
		this.Description = "Your other Undead have +1 Attack.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 2;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.HasCharge = false;
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
		return minion != this.Minion && minion.IsFriendlyOf(this.Minion) && minion.Card.MinionType == MinionType.Undead;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
