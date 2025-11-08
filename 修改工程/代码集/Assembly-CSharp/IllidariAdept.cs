using System;
using System.Linq;

public class IllidariAdept : MinionCard
{
	public IllidariAdept()
	{
		this.Name = "Illidari Adept";
		this.Description = "Evasion. Has +1/+1 for each Demon on the battlefield.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.IsEvasive = true;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyAura), new Action<Minion>(this.RemoveAura), new Func<Minion, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeMinion();
	}

	public void ApplyAura(Minion minion)
	{
		this.Minion.AddAuraAttackModifier(new Func<int, int>(this.IllidariAdeptModifier));
		this.Minion.AddAuraHealthModifier(new Func<int, int>(this.IllidariAdeptModifier), 1);
	}

	public void RemoveAura(Minion minion)
	{
	}

	public int IllidariAdeptModifier(int attack)
	{
		return attack + GameManager.Instance.GetAllMinions().Count((Minion m) => m.Card.MinionType == MinionType.Demon);
	}

	public bool ApplyCondition(Minion minion)
	{
		return minion == this.Minion;
	}

	public bool ExistCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}
}
