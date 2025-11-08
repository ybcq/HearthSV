using System;

public class GoldshirePatrol : MinionCard
{
	public GoldshirePatrol()
	{
		this.Name = "军团王牌";
		this.Description = "You can only attack if your hand is ≤1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 2;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.CantAttack = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		if (this.Player.Hand.Count <= 1)
		{
			this.Minion.CantAttack = false;
		}
	}

	public void RemoveAura(Minion minion)
	{
		this.Minion.CantAttack = true;
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion.Card.Name == this.Minion.Card.Name;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
