using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Balnazzar : MinionCard
{
	public Balnazzar()
	{
		this.Name = "巴尔纳扎";
		this.Description = "Taunt. At the start of the game, destroy and take the place of a random Legendary minion in your opponent's deck.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 6;
		this.Mechanics.OnGameStart.Add((object x) => this.OnGameStart());
		base.InitializeMinion();
	}

	public IEnumerator OnGameStart()
	{
		PreviewCardController balnazzarController = PreviewCardController.Create(this);
		balnazzarController.transform.ChangeParent(GameManager.Instance.SelfPlayer.HandController.transform);
		Animator balnazzarAnimator = balnazzarController.GetComponent<Animator>();
		balnazzarAnimator.SetTrigger("ShowNeutral");
		yield return new WaitForSeconds(1.5f);
		if (this.Player.Enemy.Deck.Any((BaseCard c) => c.Rarity == CardRarity.Legendary))
		{
			if (this.Player.IsSelf())
			{
				balnazzarAnimator.SetTrigger("NeutralToEnemyDeck");
				yield return new WaitForSeconds(0.5f);
				balnazzarController.DestroyController();
			}
			else
			{
				balnazzarAnimator.SetTrigger("NeutralToSelfDeck");
				yield return new WaitForSeconds(0.5f);
				balnazzarController.DestroyController();
			}
		}
		else
		{
			balnazzarAnimator.SetTrigger("DisappearNeutral");
			yield return new WaitForSeconds(0.25f);
			balnazzarController.DestroyController();
		}
		yield break;
	}
}
