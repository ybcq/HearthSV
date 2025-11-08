using System;

public class MoamtheDestroyer : MinionCard
{
	public MoamtheDestroyer()
	{
		this.Name = "Moam the Destroyer";
		this.Description = "Spellshield. Has +1 Attack per empty Mana Crystal for your opponent.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.General;
		this.BaseCost = 9;
		this.BaseAttack = 5;
		this.BaseHealth = 9;
		this.HasSpellshield = true;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.AddAuraAttackModifier(new Func<int, int>(this.MoamModifier));
	}

	public void RemoveAura(Minion minion)
	{
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion == this.Minion;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}

	public int MoamModifier(int attack)
	{
		return attack + this.Player.Enemy.GetUsedMana();
	}
}
