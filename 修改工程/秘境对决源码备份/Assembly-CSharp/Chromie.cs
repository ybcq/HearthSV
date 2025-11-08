using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chromie : MinionCard
{
	public Chromie()
	{
		this.Name = "守护图腾Lv4";
		this.Description = "Battlecry: Change Difficulty of this Game to Level 4.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Totem;
		this.BaseCost = 14;
		this.BaseAttack = 0;
		this.BaseHealth = 4;
		this.IsImmune = true;
		this.IsStealth = true;
		this.HasSpellshield = true;
		this.CantAttack = true;
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyMinionAura), new Action<Minion>(this.RemoveMinionAura), new Func<Minion, bool>(this.ApplyMinionCondition), new Func<bool>(this.ExistMinionCondition));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		this.Mechanics.OnGameStart.Add((object x) => this.OnGameStart());
		base.InitializeMinion();
	}

	public void ApplyMinionAura(Minion baseCard)
	{
		baseCard.AddAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier), 1);
		baseCard.HasTaunt = true;
	}

	public void RemoveMinionAura(Minion baseCard)
	{
		baseCard.RemoveAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
		baseCard.HasTaunt = false;
	}

	public int ApplyAttackModifier(int value)
	{
		return value + 1;
	}

	public int ApplyHealthModifier(int value)
	{
		return value + 1;
	}

	public bool ApplyMinionCondition(Minion minion)
	{
		return minion.Player == this.Player && minion != this.Minion;
	}

	public bool ExistMinionCondition()
	{
		return this.Minion != null && GameManager.Instance.GetAllMinions().Contains(this.Minion) && this.Minion.IsAlive();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
		yield return new WaitForSeconds(0.25f);
		yield return self.Player.SummonMinion(new Chromie());
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
			yield return this.Player.Enemy.SummonMinion(totemCard);
		}
		yield break;
	}
}
