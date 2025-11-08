using System;
using System.Collections;
using UnityEngine;

public class Ascension : SpellCard
{
	public Ascension()
	{
		this.Name = "Ascension";
		this.Description = "Your next minion costs (1) less and gains +1 Health.";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		this.CardAura = new Aura<BaseCard>(new Action<BaseCard>(this.ApplyAura), new Action<BaseCard>(this.RemoveAura), new Func<BaseCard, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		AuraManager.Instance.AddCardAura(this.CardAura);
		this.MinionPlayedSubscription = EventManager.Instance.MinionPlayedHandler.Add(new Func<MinionPlayedEvent, IEnumerator>(this.OnMinionPlayed));
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public IEnumerator OnMinionPlayed(MinionPlayedEvent minionPlayedEvent)
	{
		Minion minion = minionPlayedEvent.Minion;
		if (minion.Player == this.Player)
		{
			minion.CurrentHealth++;
			minion.AddHealthModifier(new Func<int, int>(this.AscensionModifier));
			AuraManager.Instance.RemoveCardAura(this.CardAura);
			this.MinionPlayedSubscription.Dispose();
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public int AscensionModifier(int value)
	{
		return value + 1;
	}

	public void ApplyAura(BaseCard card)
	{
		card.AddCostModifier(new Func<int, int>(this.ApplyCostModifier));
	}

	public void RemoveAura(BaseCard card)
	{
		card.RemoveCostModifier(new Func<int, int>(this.ApplyCostModifier));
	}

	public int ApplyCostModifier(int cost)
	{
		return cost - 1;
	}

	public bool ApplyCondition(BaseCard card)
	{
		return card.Player == this.Player && card is MinionCard;
	}

	public bool ExistCondition()
	{
		return true;
	}

	public IDisposable MinionPlayedSubscription;
}
