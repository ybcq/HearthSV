using System;

public class XuentheWhiteTiger : MinionCard
{
	public XuentheWhiteTiger()
	{
		this.Name = "Xuen the White Tiger";
		this.Description = "Charge. Your other characters have +2 Attack on your turn.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Legendary;
		this.BaseCost = 7;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.HasCharge = true;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyMinionAura), new Action<Minion>(this.RemoveMinionAura), new Func<Minion, bool>(this.ApplyMinionCondition), new Func<bool>(this.ExistCondition));
		this.HeroAura = new Aura<Hero>(new Action<Hero>(this.ApplyHeroAura), new Action<Hero>(this.RemoveHeroAura), new Func<Hero, bool>(this.ApplyHeroCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyMinionAura(Minion minion)
	{
		minion.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public void ApplyHeroAura(Hero hero)
	{
		hero.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public void RemoveMinionAura(Minion minion)
	{
		minion.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public void RemoveHeroAura(Hero hero)
	{
		hero.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 2;
	}

	public bool ApplyMinionCondition(Minion minion)
	{
		return minion != this.Minion && minion.IsFriendlyOf(this.Minion) && GameManager.Instance.CurrentPlayer == this.Player;
	}

	public bool ApplyHeroCondition(Hero Hero)
	{
		return Hero.Player == this.Player && GameManager.Instance.CurrentPlayer == this.Player;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
