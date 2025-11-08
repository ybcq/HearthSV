using System;

public class VeilShadowrunner : MinionCard
{
	public VeilShadowrunner()
	{
		this.Name = "面纱暗影行者";
		this.Description = "Enemy minions can't Charge.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Beast;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 4;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		minion.HasCharge = false;
	}

	public void RemoveAura(Minion minion)
	{
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion.Player == this.Player.Enemy;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
