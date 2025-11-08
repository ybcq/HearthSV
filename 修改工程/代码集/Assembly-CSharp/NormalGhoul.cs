using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NormalGhoul : MinionCard
{
	public NormalGhoul()
	{
		this.Name = "智慧图腾Lv6";
		this.Description = "Battlecry: Change Difficulty of this Game to Level 6.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Totem;
		this.BaseCost = 0;
		this.BaseAttack = 0;
		this.BaseHealth = 6;
		this.IsImmune = true;
		this.IsStealth = true;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyMinionAura), new Action<Minion>(this.RemoveMinionAura), new Func<Minion, bool>(this.ApplyMinionCondition), new Func<bool>(this.ExistMinionCondition));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		this.Mechanics.OnGameStart.Add((object x) => this.OnGameStart());
		base.InitializeMinion();
	}

	public void ApplyMinionAura(Minion baseCard)
	{
		baseCard.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
		baseCard.AddAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier), 2);
	}

	public void RemoveMinionAura(Minion baseCard)
	{
		baseCard.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
		baseCard.RemoveAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
	}

	public int ApplyAttackModifier(int value)
	{
		return value + 1;
	}

	public int ApplyHealthModifier(int value)
	{
		return value + 2;
	}

	public bool ApplyMinionCondition(Minion minion)
	{
		return minion.Player == this.Player && minion != this.Minion;
	}

	public bool ExistMinionCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			yield return this.Player.Draw(1, null);
			yield break;
		}
		yield break;
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.SummonMinion(new NormalGhoul());
		yield break;
	}

	public IEnumerator OnGameStart()
	{
		PreviewCardController diffController = PreviewCardController.Create(this);
		diffController.transform.ChangeParent(GameManager.Instance.SelfPlayer.HandController.transform);
		Animator diffAnimator = diffController.GetComponent<Animator>();
		diffAnimator.SetTrigger("ShowNeutral");
		yield return new WaitForSeconds(1.5f);
		diffAnimator.SetTrigger("DisappearNeutral");
		yield return new WaitForSeconds(0.25f);
		diffController.DestroyController();
		List<MinionCard> characters = (from c in this.Player.Deck.OfType<MinionCard>()
		where c.MinionType == MinionType.Totem
		select c).ToList<MinionCard>();
		MinionCard totemCard = RNG.RandomItemFrom<MinionCard>(characters);
		if (totemCard != null)
		{
			yield return this.Player.DrawFromDeck(totemCard, null);
			this.Player.RemoveCardFromHand(totemCard);
			yield return this.Player.SummonMinion(totemCard);
		}
		yield break;
	}
}
