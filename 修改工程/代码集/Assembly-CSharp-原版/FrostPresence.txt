using System;
using System.Collections;

public class FrostPresence : SpellCard
{
	public FrostPresence()
	{
		this.Name = "Frost Presence";
		this.Description = "Your Freeze cards cost (1) less.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		this.CardAura = new Aura<BaseCard>(new Action<BaseCard>(this.ApplyAura), new Action<BaseCard>(this.RemoveAura), new Func<BaseCard, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeSpell();
	}

	public void ApplyAura(BaseCard baseCard)
	{
		baseCard.AddCostModifier(new Func<int, int>(this.ApplyCostModifier));
	}

	public void RemoveAura(BaseCard baseCard)
	{
		baseCard.RemoveCostModifier(new Func<int, int>(this.ApplyCostModifier));
	}

	public int ApplyCostModifier(int cost)
	{
		return cost - 1;
	}

	public bool ApplyCondition(BaseCard baseCard)
	{
		return baseCard.Player == this.Player && (baseCard.Name.Contains("Freeze") || baseCard.Name.Contains("Frozen") || baseCard.Description.Contains("Freeze") || baseCard.Description.Contains("Frozen"));
	}

	public bool ExistCondition()
	{
		return true;
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.SetPresence(new DisposableAura<BaseCard>(this.CardAura), Presence.Frost);
		yield break;
	}
}
