BonyConstruct LV1 生命值+1
ChargeGhoul LV2 生命值攻击力+1
DancingRuneblade LV3 生命值+2/攻击力+1

FallenChampion LV4 抽一张牌 生命值+1
GhostriderofKarabor LV5 抽一张牌 生命值攻击力+1
NormalGhoul LV6 抽一张牌 生命值+2/攻击力+1

PartyCrashers LV7 抽一张牌 生命值+1 剧毒
RaisedGhoul LV8 抽一张牌 生命值攻击力+1 分裂
Swarmling LV9 抽一张牌 生命值+2/攻击力+1 风怒

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class 卡名 : MinionCard
{
	public 卡名()
	{
    this.Name = "智慧图腾Lv1";
		this.Description = "Battlecry: Change Difficulty of this Game to Level 1.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Totem;
		this.BaseCost = 0;
		this.BaseAttack = 0;
		this.BaseHealth = 9;
		this.IsImmune = true;
		this.IsStealth = true;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		this.MinionAura = new Aura<Minion>(new Action<Minion>(this.ApplyMinionAura), new Action<Minion>(this.RemoveMinionAura), new Func<Minion, bool>(this.ApplyMinionCondition), new Func<bool>(this.ExistMinionCondition));
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));//亡语，回到手牌
		this.Mechanics.OnGameStart.Add((object x) => this.OnGameStart());//回合开始时
		base.InitializeMinion();
		
	}
	public IEnumerator OnGameStart()
	{
		PreviewCardController diffController = PreviewCardController.Create(this);
		diffController.transform.ChangeParent(GameManager.Instance.SelfPlayer.HandController.transform);//洗牌动作
		Animator diffAnimator = diffController.GetComponent<Animator>();
		diffAnimator.SetTrigger("ShowNeutral");
		yield return new WaitForSeconds(1.5f);
		diffAnimator.SetTrigger("DisappearNeutral");//消失动作
		yield return new WaitForSeconds(0.25f);
		diffController.DestroyController();
		//开始找图腾
		List<MinionCard> totems = (from c in this.Player.Deck.OfType<MinionCard>()
		where c.MinionType == MinionType.Totem
		select c).ToList<MinionCard>();
		MinionCard totemCard = RNG.RandomItemFrom<MinionCard>(totems);
		if (totemCard != null)
		{
			yield return this.Player.DrawFromDeck(totemCard, null);//抽上来
			this.Player.RemoveCardFromHand(totemCard);//扔了
			yield return this.Player.SummonMinion(totemCard);//召唤
		}
		yield break;
	}
  public IEnumerator Deathrattle(Minion self)
	{
		yield return self.Player.SummonMinion(new 卡名());
		yield break;
	}
	public void ApplyMinionAura(Minion baseCard)
	{
		baseCard.AddAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));//攻击力
		baseCard.AddAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier), 1);//生命值
		baseCard.As<Minion>().HasSpellshield = true;//特效
	}

	public void RemoveMinionAura(Minion baseCard)
	{
		baseCard.RemoveAuraAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
		baseCard.RemoveAuraHealthModifier(new Func<int, int>(this.ApplyHealthModifier));
		baseCard.As<Minion>().HasSpellshield = false;//特效
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
		return minion.Player == this.Player;
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
	}
}

//旧版神龛
LV1 神龛 抽1张牌
LV2 神龛 抽1张牌，你的法术牌消耗-1
LV3 神龛 抽2张牌，你的法术牌消耗-1
LV4 神龛 抽2张牌，你的法术牌消耗-1，你的随从牌消耗-1
LV5 神龛 抽3张牌，你的法术牌消耗-1，你的随从牌消耗-1
LV6 神龛 抽3张牌，你的法术牌消耗-1，你的随从牌消耗-1，你的随从牌的血量+1
LV7 神龛 抽4张牌，你的法术牌消耗-1，你的随从牌消耗-1，你的随从牌的血量+1
PartyCrashers LV8 神龛 抽5张牌，你的法术牌消耗-1，你的随从牌消耗-1，你的随从牌的血量+1
BonyConstruct LV9 神龛 抽5张牌，你的法术牌消耗-1，你的随从牌消耗-1，你的随从牌的血量+1，攻击力+1
