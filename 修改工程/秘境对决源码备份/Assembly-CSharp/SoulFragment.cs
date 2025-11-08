using System;

public class SoulFragment : MinionCard
{
	public SoulFragment()
	{
		this.Name = "利维坦";
		this.Description = "Can't attack until you being powerful.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 4;
		this.BaseAttack = 6;
		this.BaseHealth = 5;
		this.CantAttack = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		if (this.Player.TurnMana >= 7)
		{
			minion.CantAttack = false;
		}
	}

	public void RemoveAura(Minion minion)
	{
		minion.CantAttack = true;
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
