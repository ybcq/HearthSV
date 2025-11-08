using System;

public class ScarletProtector : MinionCard
{
	public ScarletProtector()
	{
		this.Name = "菲利克斯·掠日者";
		this.Description = "Your other minions are Immune.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 10;
		this.BaseAttack = 10;
		this.BaseHealth = 10;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.As<Minion>().IsImmune = true;
	}

	public void RemoveAura(Minion minion)
	{
		minion.As<Minion>().IsImmune = false;
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
