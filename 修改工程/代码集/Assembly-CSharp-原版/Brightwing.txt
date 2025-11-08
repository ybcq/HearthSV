using System;

public class Brightwing : MinionCard
{
	public Brightwing()
	{
		this.Name = "Brightwing";
		this.Description = "Your hero can't be targeted by spells or Hero Powers.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 1;
		this.BaseAttack = 1;
		this.BaseHealth = 3;
		this.HeroAura = new Aura<Hero>(new Action<Hero>(this.ApplyAura), new Action<Hero>(this.RemoveAura), new Func<Hero, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Hero hero)
	{
		hero.HasSpellshield = true;
	}

	public void RemoveAura(Hero hero)
	{
		hero.HasSpellshield = false;
	}

	public bool ApplyCondition(Hero hero)
	{
		return hero == this.Minion.Player.Hero;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
