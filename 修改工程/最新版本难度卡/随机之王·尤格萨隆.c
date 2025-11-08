using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chromie : SpellCard
{
	public Chromie()
	{
		this.Name = "挑战随机之王·尤格萨隆";
		this.Description = "Always Lucky.";
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
		
		yield return EventManager.Instance.TurnStartHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		
		//移除这张卡
		this.Player.RemoveCardFromDeck(this);
		BaseCard addCard = RNG.RandomItemFrom<BaseCard>((from c in CardManager.Instance.AllCards
                        where c.Class == this.Player.Hero.Class
                        select c).ToList<BaseCard>());
    this.Player.AddCardToDeck(addCard);
		yield break;
}			
  public IEnumerator OnTurnStart(TurnEvent turnEvent)
  {
    if (turnEvent.Player == this.Player)
		{
      //优先解场，群解法术
      if(this.Player.Enemy.Minions.Count >= 4){
        SpellCard card = RNG.RandomItemFrom<SpellCard>((from m in CardManager.Instance.AllCards.OfType<SpellCard>()
                   where (m.Description.Contains("All") || m.Description.Contains("all")) && (m.Description.Contains("nemy") || m.Description.Contains("nemies"))
                   select m).ToList<SpellCard>());
        yield return this.Player.AddCardToHand(card);
        yield break;
      }
      //优先治疗，单体法术
      else if(this.Player.Hero.CurrentHealth < 10){
        SpellCard card = RNG.RandomItemFrom<SpellCard>((from m in CardManager.Instance.AllCards.OfType<SpellCard>()
                   where (m.Description.Contains("heal") || m.Description.Contains("Heal") || m.Description.Contains("estore") || m.Description.Contains("ive"))
                   select m).ToList<SpellCard>());
        yield return this.Player.AddCardToHand(card);
        yield break;
      }
      //优先Buff，群体法术
      else if(this.Player.Minions.Count >= 2){
        SpellCard card = RNG.RandomItemFrom<SpellCard>((from m in CardManager.Instance.AllCards.OfType<SpellCard>()
                   where (m.Description.Contains("our"))
                   select m).ToList<SpellCard>());
        yield return this.Player.AddCardToHand(card);
        yield break;
      }
      //优先伤害，打脸输出
      else{
        SpellCard card = RNG.RandomItemFrom<SpellCard>((from m in CardManager.Instance.AllCards.OfType<SpellCard>()
                   where (m.Description.Contains("amage") || m.Description.Contains("Deal") || m.Description.Contains("deal") || m.Description.Contains("estroy"))
                   select m).ToList<SpellCard>());
        yield return this.Player.AddCardToHand(card);
        yield break;
      }
    }
    yield break;
  }				
}
