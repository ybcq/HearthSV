using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseAI : MonoBehaviour
{
	public void DoTurn()
	{
		base.StartCoroutine(this.Think());
	}

	public abstract IEnumerator Think();

	public List<BaseCard> GetPriorityCards(OrderPriority priority)
	{
		return (from c in this.Player.Hand
		where this.SpecialOrderRules.ContainsKey(c.Name) && this.SpecialOrderRules[c.Name] == priority
		select c).ToList<BaseCard>();
	}

	public List<Minion> GetTargeteableEnemyMinions(Character attacker)
	{
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where attacker.CanAttackTo(m) && m.HasTaunt && !m.IsImmune && m.IsAlive()
		select m).ToList<Minion>();
		if (list.Count > 0)
		{
			return list;
		}
		return (from m in this.Player.Enemy.Minions
		where attacker.CanAttackTo(m) && !m.IsImmune && m.IsAlive()
		select m).ToList<Minion>();
	}

	public Character GetRandomEnemyMinion()
	{
		List<Minion> list = (from m in this.Player.Enemy.Minions
		where m.HasTaunt && !m.IsStealth && !m.IsImmune && m.IsAlive()
		select m).ToList<Minion>();
		if (list.Count > 0)
		{
			return RNG.RandomItemFrom<Minion>(list);
		}
		return RNG.RandomItemFrom<Minion>((from m in this.Player.Enemy.Minions
		where !m.IsStealth && !m.IsImmune && m.IsAlive()
		select m).ToList<Minion>());
	}

	public IEnumerator PlayUnorderedCards()
	{
		List<BaseCard> cardList = (from c in this.Player.Hand
		where !this.SpecialOrderRules.ContainsKey(c.Name)
		orderby c.CurrentCost descending
		select c).ToList<BaseCard>();
		yield return this.PlayCardsInList(cardList);
		yield break;
	}

	public IEnumerator PlayCardsWith(OrderPriority priority)
	{
		List<BaseCard> cardList = this.GetPriorityCards(priority);
		yield return this.PlayCardsInList(cardList);
		yield break;
	}

	private IEnumerator PlayCardsInList(List<BaseCard> cardList)
	{
		foreach (BaseCard card in cardList)
		{
			if (this.Player.HasManaToPlay(card))
			{
				if (!(card is MinionCard) || this.Player.Minions.Count < 7)
				{
					if (!(card is WeaponCard) || !this.Player.HasWeapon())
					{
						if (!(card is SpellCard) || card.As<SpellCard>().CanCast())
						{
							if (!this.SpecialPlayRules.ContainsKey(card.Name) || this.SpecialPlayRules[card.Name]())
							{
								if (this.SpecialComboConditions.ContainsKey(card.Name))
								{
									if (this.SpecialComboConditions[card.Name]())
									{
										this.SpecialCombos[card.Name]();
									}
								}
								else
								{
									yield return this.Play(card);
								}
							}
						}
					}
				}
			}
		}
		yield break;
	}

	private IEnumerator Play(BaseCard card)
	{
		CardType cardType = card.GetCardType();
		if (cardType != CardType.Minion)
		{
			if (cardType != CardType.Spell)
			{
				if (cardType == CardType.Weapon)
				{
					if (!this.Player.CanPlayWeapons)
					{
						yield break;
					}
					yield return card.As<WeaponCard>().Play(null);
				}
			}
			else
			{
				if (!this.Player.CanPlaySpells)
				{
					yield break;
				}
				SpellCard spellCard = card.As<SpellCard>();
				if (spellCard.TargetType == TargetType.NoTarget)
				{
					yield return spellCard.PlayOn(null);
				}
				else
				{
					List<Character> targeteableMinions = (from c in GameManager.Instance.GetAllCharacters().TargeteablesBySpellOf(this.Player)
					where spellCard.CanTarget(c)
					select c).ToList<Character>();
					if (this.SpecialTargetRules.ContainsKey(spellCard.Name))
					{
						Func<List<Character>, Character> targetRule = this.SpecialTargetRules[spellCard.Name];
						Character target = targetRule(targeteableMinions);
						if (target != null)
						{
							yield return spellCard.PlayOn(target);
						}
					}
					else
					{
						Character target2 = RNG.RandomItemFrom<Character>(targeteableMinions);
						yield return spellCard.PlayOn(target2);
					}
				}
			}
		}
		else
		{
			if (!this.Player.CanPlayMinions)
			{
				yield break;
			}
			MinionCard minionCard = card.As<MinionCard>();
			if (this.Player.Minions.Count < 7)
			{
				this.Player.RemoveCardFromHand(card);
				if (this.SpecialPositionRules.ContainsKey(card.Name))
				{
					PositionPriority positionPriority = this.SpecialPositionRules[card.Name];
					if (positionPriority != PositionPriority.Left)
					{
						if (positionPriority != PositionPriority.Right)
						{
							if (positionPriority == PositionPriority.Middle)
							{
								minionCard.Minion = this.Player.AddMinionToBoard(minionCard, (int)Math.Ceiling((double)this.Player.Minions.Count / 2.0));
							}
						}
						else
						{
							minionCard.Minion = this.Player.AddMinionToBoard(minionCard, this.Player.Minions.Count);
						}
					}
					else
					{
						minionCard.Minion = this.Player.AddMinionToBoard(minionCard, 0);
					}
				}
				else
				{
					minionCard.Minion = this.Player.AddMinionToBoard(minionCard, RNG.RandomInteger(0, this.Player.Minions.Count));
				}
				yield return minionCard.PlayOn(null);
			}
		}
		yield break;
	}

	public void Stop()
	{
		base.StopAllCoroutines();
	}

	public Player Player;

	public Dictionary<string, Func<bool>> SpecialPlayRules = new Dictionary<string, Func<bool>>();

	public Dictionary<string, Func<List<Character>, Character>> SpecialTargetRules = new Dictionary<string, Func<List<Character>, Character>>();

	public Dictionary<string, Func<bool>> SpecialComboConditions = new Dictionary<string, Func<bool>>();

	public Dictionary<string, Func<IEnumerator>> SpecialCombos = new Dictionary<string, Func<IEnumerator>>();

	public Dictionary<string, PositionPriority> SpecialPositionRules = new Dictionary<string, PositionPriority>();

	public Dictionary<string, OrderPriority> SpecialOrderRules = new Dictionary<string, OrderPriority>();
}
