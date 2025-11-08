using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class WarmaulReaver : SpellCard
{
	public WarmaulReaver()
	{
		this.Name = "挑战冲锋之王·火车王";
		this.Description = "All minions with attack of less than 6 of your opponent have Charge. Summon two young dragons for you.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.BaseCost = 20;
		this.TargetType = TargetType.NoTarget;
		base.InitializeSpell();
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
		RNG.RandomItemFrom<MinionCard>((from c in this.Player.Deck.OfType<MinionCard>()
		where c.MinionType == MinionType.Totem
		select c).ToList<MinionCard>());
		foreach (Minion minion in this.Player.Enemy.Minions)
		{
			if (minion.CurrentAttack <= 6)
			{
				minion.HasCharge = true;
			}
		}
		foreach (MinionCard minionCard in this.Player.Enemy.Hand.OfType<MinionCard>())
		{
			if (minionCard.BaseAttack <= 6)
			{
				minionCard.HasCharge = true;
			}
		}
		foreach (MinionCard minionCard2 in this.Player.Enemy.Deck.OfType<MinionCard>())
		{
			if (minionCard2.BaseAttack <= 6)
			{
				minionCard2.HasCharge = true;
			}
		}
		RockjawBonepicker rockjawBonepicker = new RockjawBonepicker
		{
			BaseHealth = 1,
			BaseAttack = 1
		};
		yield return this.Player.SummonMinion(rockjawBonepicker);
		RockjawBonepicker rockjawBonepicker2 = new RockjawBonepicker
		{
			BaseHealth = 1,
			BaseAttack = 1
		};
		yield return this.Player.SummonMinion(rockjawBonepicker2);
		this.Player.RemoveCardFromDeck(this);
		BaseCard addCard = RNG.RandomItemFrom<BaseCard>((from c in CardManager.Instance.AllCards
		where c.Class == this.Player.Hero.Class
		select c).ToList<BaseCard>());
		this.Player.AddCardToDeck(addCard);
		yield break;
	}
}
