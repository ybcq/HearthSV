using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScaleswornSkyrazor : MinionCard
{
	public ScaleswornSkyrazor()
	{
		this.Name = "平衡图腾Lv5";
		this.Description = "Battlecry: Change Difficulty of this Game to Level 5.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Totem;
		this.BaseCost = 15;
		this.BaseAttack = 0;
		this.BaseHealth = 5;
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
		baseCard.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
		baseCard.AddAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier), 5);
	}

	public void RemoveMinionAura(Minion baseCard)
	{
		baseCard.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
		baseCard.RemoveAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
	}

	public int ApplyAttackModifier(int value)
	{
		return 4;
	}

	public int ApplyHealthModifier(int value)
	{
		return 4;
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
		yield return self.Player.SummonMinion(new ScaleswornSkyrazor());
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
