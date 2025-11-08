using System;

public class Silence : SpellCard
{
	public Silence()
	{
		this.Name = "Silence";
		this.Description = "Held: You can't use your Hero Power or cast other spells.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 6;
		this.Collectible = false;
		this.HeroAura = new Aura<Hero>(new Action<Hero>(this.ApplyAura), new Action<Hero>(this.RemoveAura), new Func<Hero, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeSpell();
	}

	public void ApplyAura(Hero hero)
	{
		hero.Player.CanHeroPower = false;
		hero.Player.CanPlaySpells = false;
	}

	public void RemoveAura(Hero hero)
	{
		hero.Player.CanHeroPower = true;
		hero.Player.CanPlaySpells = true;
	}

	public bool ApplyCondition(Hero hero)
	{
		return hero.Player == this.Player;
	}

	public bool ExistCondition()
	{
		return this.Player.Hand.Contains(this);
	}
}
