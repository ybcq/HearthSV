using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Silence : SpellCard
{
	public Silence()
	{
		this.Name = "测试模式";
		this.Description = "Test Mode.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		this.Collectible = false;
		this.Mechanics.OnGameStart.Add((object x) => this.OnGameStart());
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.Hero.IsImmune = true;
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
		SpellCard spellCard = RNG.RandomItemFrom<SpellCard>((from c in this.Player.Deck.OfType<SpellCard>()
		where c.Name == "测试模式"
		select c).ToList<SpellCard>());
		if (spellCard != null)
		{
			yield return this.Player.DrawFromDeck(spellCard, null);
		}
		yield break;
	}
}
