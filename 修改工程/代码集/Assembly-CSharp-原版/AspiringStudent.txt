using System;

public class AspiringStudent : MinionCard
{
	public AspiringStudent()
	{
		this.Name = "Aspiring Student";
		this.Description = "Your hero has +1 Attack on your turn.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.HeroAura = new Aura<Hero>(new Action<Hero>(this.ApplyAura), new Action<Hero>(this.RemoveAura), new Func<Hero, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Hero hero)
	{
		hero.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public void RemoveAura(Hero hero)
	{
		hero.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 1;
	}

	public bool ApplyCondition(Hero hero)
	{
		return hero == this.Minion.Player.Hero && GameManager.Instance.CurrentPlayer == this.Minion.Player;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
