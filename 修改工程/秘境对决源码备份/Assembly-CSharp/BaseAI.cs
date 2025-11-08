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
		foreach (BaseCard baseCard in cardList)
		{
			if (this.Player.HasManaToPlay(baseCard))
			{
				if ((!(baseCard is MinionCard) || this.Player.Minions.Count < 7) && (!(baseCard is WeaponCard) || !this.Player.HasWeapon()) && (!(baseCard is SpellCard) || baseCard.As<SpellCard>().CanCast()) && (!this.SpecialPlayRules.ContainsKey(baseCard.Name) || this.SpecialPlayRules[baseCard.Name]()))
				{
					if (this.SpecialComboConditions.ContainsKey(baseCard.Name))
					{
						if (this.SpecialComboConditions[baseCard.Name]())
						{
							this.SpecialCombos[baseCard.Name]();
						}
					}
					else
					{
						yield return this.Play(baseCard);
					}
				}
				yield return new WaitForSeconds(0.25f);
			}
		}
		List<BaseCard>.Enumerator enumerator = default(List<BaseCard>.Enumerator);
		yield break;
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
					if (spellCard.Description.Contains("All") || spellCard.Description.Contains("all"))
					{
						if (spellCard.Description.Contains("nemy") || spellCard.Description.Contains("nemies"))
						{
							if (this.Player.Enemy.Minions.Count >= 3)
							{
								yield return spellCard.PlayOn(null);
							}
						}
						else if (spellCard.Description.Contains("our"))
						{
							if (this.Player.Minions.Count >= 3)
							{
								yield return spellCard.PlayOn(null);
							}
						}
						else if (this.Player.Enemy.Minions.Count >= this.Player.Minions.Count + 2)
						{
							yield return spellCard.PlayOn(null);
						}
					}
					else
					{
						yield return spellCard.PlayOn(null);
					}
				}
				else
				{
					List<Character> list = (from c in GameManager.Instance.GetAllCharacters().TargeteablesBySpellOf(this.Player)
					where spellCard.CanTarget(c)
					select c).ToList<Character>();
					if (this.SpecialTargetRules.ContainsKey(spellCard.Name))
					{
						Character character = this.SpecialTargetRules[spellCard.Name](list);
						if (character != null)
						{
							yield return spellCard.PlayOn(character);
						}
					}
					else
					{
						List<Character> characters = (from c in GameManager.Instance.GetAllCharacters().TargeteablesBySpellOf(this.Player)
						where spellCard.CanTarget(c) && spellCard.Player.Hero.IsEnemyOf(c)
						select c).ToList<Character>();
						List<Character> characters2 = (from c in GameManager.Instance.GetAllCharacters().TargeteablesBySpellOf(this.Player)
						where spellCard.CanTarget(c) && spellCard.Player.Hero.IsFriendlyOf(c)
						select c).ToList<Character>();
						Character character2 = RNG.RandomItemFrom<Character>(characters);
						Character character3 = RNG.RandomItemFrom<Character>(characters2);
						Character character4 = RNG.RandomItemFrom<Character>(list);
						if (spellCard.Description.Contains("heal") || spellCard.Description.Contains("Heal") || spellCard.Description.Contains("estore") || spellCard.Description.Contains("ive"))
						{
							if (character3 != null)
							{
								yield return spellCard.PlayOn(character3);
							}
						}
						else if (spellCard.Description.Contains("amage") || spellCard.Description.Contains("Deal") || spellCard.Description.Contains("deal") || spellCard.Description.Contains("estroy"))
						{
							if (character2 != null)
							{
								yield return spellCard.PlayOn(character2);
							}
						}
						else if (character4 != null)
						{
							yield return spellCard.PlayOn(character4);
						}
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
				if (minionCard.Description.Contains("All") || minionCard.Description.Contains("all"))
				{
					if (minionCard.Description.Contains("nemy") || minionCard.Description.Contains("nemies"))
					{
						if (this.Player.Enemy.Minions.Count >= 2)
						{
							yield return minionCard.PlayOn(null);
						}
					}
					else if (minionCard.Description.Contains("our"))
					{
						if (this.Player.Minions.Count >= 2)
						{
							yield return minionCard.PlayOn(null);
						}
					}
					else if (this.Player.Enemy.Minions.Count >= this.Player.Minions.Count + 2)
					{
						yield return minionCard.PlayOn(null);
					}
				}
				else
				{
					yield return minionCard.PlayOn(null);
				}
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
