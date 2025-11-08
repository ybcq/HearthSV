using System;

public class Shardling : MinionCard
{
	public Shardling()
	{
		this.Name = "伊莉雅，冬拥龙语姬";
		this.Description = "The opponent's frozen creature has 0 power.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Epic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 5;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public void RemoveAura(Minion minion)
	{
		minion.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
	}

	public int ApplyAttackModifier(int attack)
	{
		return 0;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion.IsEnemyOf(this.Player.Hero) && minion.Card.MinionType == MinionType.Biol && minion.IsFrozen;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
