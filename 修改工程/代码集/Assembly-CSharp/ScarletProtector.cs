using System;

public class ScarletProtector : MinionCard
{
	public ScarletProtector()
	{
		this.Name = "Yong Woo";
		this.Description = "Your other minions have +3 Attack and Charge.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.AddAuraAttackModifier(new Func<int, int>(this.ApplyModifier));
		minion.As<Minion>().HasCharge = true;
	}

	public void RemoveAura(Minion minion)
	{
		minion.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyModifier));
		minion.As<Minion>().HasCharge = false;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion.Player == this.Player && minion != this.Minion;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}

	public int ApplyModifier(int attack)
	{
		return attack + 3;
	}
}
