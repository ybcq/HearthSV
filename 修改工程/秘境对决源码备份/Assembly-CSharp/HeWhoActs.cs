using System;

public class HeWhoActs : MinionCard
{
	public HeWhoActs()
	{
		this.Name = "黑龙骑士·法露特";
		this.Description = "Charge. if you are powerful, it become Immune";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 1;
		this.HasCharge = true;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyMinionAura), new Action<Minion>(this.RemoveMinionAura), new Func<Minion, bool>(this.ApplyMinionCondition), new Func<bool>(this.ExistMinionCondition));
		base.InitializeMinion();
	}

	public void ApplyMinionAura(Minion baseCard)
	{
		this.Minion.HasWuMian = true;
	}

	public void RemoveMinionAura(Minion baseCard)
	{
		if (this.Player.TurnMana <= 7)
		{
			this.Minion.HasWuMian = false;
		}
	}

	public bool ApplyMinionCondition(Minion minion)
	{
		return minion == this.Minion;
	}

	public bool ExistMinionCondition()
	{
		return this.Minion != null && this.Minion.IsAlive() && this.Player.TurnMana >= 7;
	}
}
