using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class TheHeadlessHorseman : SpellCard
{
	public TheHeadlessHorseman()
	{
		this.Name = "挑战溅射之王·多头蛇";
		this.Description = "All minions of your opponent have Cleave";
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
			minion.HasCleave = true;
		}
		foreach (MinionCard minionCard in this.Player.Enemy.Hand.OfType<MinionCard>())
		{
			minionCard.HasCleave = true;
		}
		foreach (MinionCard minionCard2 in this.Player.Enemy.Deck.OfType<MinionCard>())
		{
			minionCard2.HasCleave = true;
		}
		this.Player.RemoveCardFromDeck(this);
		BaseCard addCard = RNG.RandomItemFrom<BaseCard>((from c in CardManager.Instance.AllCards
		where c.Class == this.Player.Hero.Class
		select c).ToList<BaseCard>());
		this.Player.AddCardToDeck(addCard);
		yield break;
	}
}
