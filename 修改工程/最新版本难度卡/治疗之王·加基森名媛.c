using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DisgruntledGrunt : SpellCard
{
	public DisgruntledGrunt()
	{
		this.Name = "挑战治疗之王·加基森名媛";
		this.Description = "Your opponent gains powerful healing skills.";
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
		yield return this.Player.Enemy.ReplaceHeroPower(new MetamorphosisHP(this.Player.Enemy.Hero));
		this.Player.RemoveCardFromDeck(this);
		BaseCard addCard = RNG.RandomItemFrom<BaseCard>((from c in CardManager.Instance.AllCards
		where c.Class == this.Player.Hero.Class
		select c).ToList<BaseCard>());
		this.Player.AddCardToDeck(addCard);
		yield break;
	}
}
