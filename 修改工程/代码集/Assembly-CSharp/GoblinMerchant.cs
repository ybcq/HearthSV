using System;

public class GoblinMerchant : MinionCard
{
	public GoblinMerchant()
	{
		this.Name = "地精商人";
		this.Description = "Your hero power costs (0).";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.HeroPowerAura = new Aura<BaseHeroPower>(new Action<BaseHeroPower>(this.ApplyAura), new Action<BaseHeroPower>(this.RemoveAura), new Func<BaseHeroPower, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(BaseHeroPower heroPower)
	{
		heroPower.AddAuraCostModifier(new Func<int, int>(this.ApplyCostModifier));
	}

	public void RemoveAura(BaseHeroPower heroPower)
	{
		heroPower.RemoveAuraCostModifier(new Func<int, int>(this.ApplyCostModifier));
	}

	public int ApplyCostModifier(int cost)
	{
		return 0;
	}

	public bool ApplyCondition(BaseHeroPower heroPower)
	{
		return heroPower.Hero.Player == this.Player;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
