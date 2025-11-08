using System;
using System.Linq;

public class Arcaneling : MinionCard
{
	public Arcaneling()
	{
		this.Name = "冰沼水灵";
		this.Description = "Freeze creatures damaged by this creature's battle. Whenever a creature is frozen, gain +2 strength.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.HasFreeze = true;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		this.minions = 0;
		foreach (Minion minion2 in (from m in this.Player.Minions
		where m.IsFrozen && m.Card.MinionType == MinionType.Biol
		select m).ToList<Minion>())
		{
			this.minions++;
		}
		this.Minion.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public void RemoveAura(Minion minion)
	{
		this.Minion.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 2 * this.minions;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion == this.Minion;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}

	public int minions;
}
