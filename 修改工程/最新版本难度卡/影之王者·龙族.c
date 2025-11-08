using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpirestoneMagus : SpellCard
{
	public SpirestoneMagus()
	{
		this.Name = "挑战影之王者·龙族";
		this.Description = "Challenge the King of Shadows Dragon Race";
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
		yield return this.Player.Enemy.ReplaceHero(null, new VaredisFelsoul());
		yield return this.Player.Enemy.ReplaceHeroPower(new DarkPact(this.Player.Enemy.Hero));
		int CardCount = 1;
		int AddCount = 0;
		while (CardCount > 0)
		{
			List<BaseCard> Othercards = (from c in this.Player.Enemy.Deck
			where c.Class > HeroClass.Neutral
			select c).ToList<BaseCard>();
			if (Othercards.Count > 0)
			{
				BaseCard randomCard = RNG.RandomItemFrom<BaseCard>(Othercards);
				this.Player.Enemy.RemoveCardFromDeck(randomCard);
				AddCount++;
			}
			CardCount = Othercards.Count;
		}
		for (int i = 0; i < AddCount; i++)
		{
			BaseCard addCard = RNG.RandomItemFrom<BaseCard>((from c in CardManager.Instance.AllCards
			where c.Class == HeroClass.DeathKnight
			select c).ToList<BaseCard>());
			this.Player.Enemy.AddCardToDeck(addCard);
		}
		this.Player.RemoveCardFromDeck(this);
		BaseCard addCard2 = RNG.RandomItemFrom<BaseCard>((from c in CardManager.Instance.AllCards
		where c.Class == this.Player.Hero.Class
		select c).ToList<BaseCard>());
		this.Player.AddCardToDeck(addCard2);
		yield break;
	}
}
