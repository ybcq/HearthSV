using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Player()
	{
	}

	public static Player Create(PlayerParameters parameters)
	{
		Player player = new GameObject("Player_" + parameters.Hero.Name)
		{
			transform = 
			{
				localScale = Vector3.one * 50f,
				localEulerAngles = new Vector3(90f, 0f, 0f),
				position = parameters.PlayerPosition
			}
		}.AddComponent<Player>();
		player.IsEnemy = parameters.IsEnemy;
		player.Deck = parameters.Deck;
		foreach (BaseCard baseCard in player.Deck)
		{
			baseCard.Player = player;
		}
		player.ManaController = ManaController.Create(player, parameters.ManaPosition, parameters.DisplayCrystals);
		player.HandController = HandController.Create(player, parameters.HandPosition, parameters.IsEnemy);
		player.BoardController = BoardController.Create(player, parameters.BoardPosition, parameters.IsEnemy);
		player.MeditateController = MeditateController.Create(player);
		player.Hero = (Hero)Activator.CreateInstance(parameters.Hero);
		player.Hero.Player = player;
		player.Hero.Initialize();
		player.Hero.Controller = HeroController.Create(player.Hero, parameters.IsEnemy);
		player.Hero.HeroPower = player.Hero.GetDefaultHeroPower();
		player.Hero.HeroPower.Controller = HeroPowerController.Create(player.Hero.HeroPower, parameters.IsEnemy);
		return player;
	}

	public IEnumerator PlayMinion(Minion minion, Character target)
	{
		Debugger.LogPlayer(this, "playing minion " + minion.Card.Name);
		this.Minions.Add(minion);
		SoundManager.Instance.PlayMinionSound(minion.Card, "Spawn", 0.5f);
		SoundManager.Instance.PlayDropSound(minion.Card.BaseCost);
		if (minion.Mechanics.HasBattlecry() && minion.Card.CanBattlecry())
		{
			if (minion.Card.BattlecryType == BattlecryType.NoTarget)
			{
				yield return minion.Mechanics.Battlecry.Fire(null);
			}
			else if (target != null)
			{
				yield return minion.Mechanics.Battlecry.Fire(target);
			}
		}
		if (minion.Mechanics.HasMeditate())
		{
			this.AddMeditation(minion.Card);
		}
		yield return EventManager.Instance.OnMinionPlayed(this, minion);
		yield return EventManager.Instance.OnMinionSummoned(this, minion);
		yield return EventManager.Instance.OnCardPlayed(this, minion.Card);
		this.EnableAuras(minion.Card);
		this.PlayedMinions.Add(minion.Card);
		this.SummonedMinions.Add(minion.Card);
		GameManager.Instance.GameUpdate();
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	public IEnumerator SummonMinion(MinionCard minionCard)
	{
		yield return this.SummonMinion(minionCard, this.Minions.Count);
		yield break;
	}

	public IEnumerator SummonMinion(MinionCard minionCard, int position)
	{
		Debugger.LogPlayer(this, string.Concat(new object[]
		{
			"summoning ",
			minionCard.Name,
			" at position ",
			position
		}));
		if (this.Minions.Count < 7)
		{
			minionCard.SetOwner(this);
			Minion minion = this.AddMinionToBoard(minionCard, position);
			this.Minions.Add(minion);
			SoundManager.Instance.PlayMinionSound(minion.Card, "Spawn", 0.5f);
			yield return EventManager.Instance.OnMinionSummoned(this, minion);
			this.EnableAuras(minion.Card);
			this.SummonedMinions.Add(minionCard);
			yield return new WaitForSeconds(0.5f);
		}
		else
		{
			Debugger.LogPlayer(this, "couldn't summon " + minionCard.Name + " because board is full");
		}
		GameManager.Instance.GameUpdate();
		yield break;
	}

	public Minion AddMinionToBoard(MinionCard minionCard, int position)
	{
		Minion minion = new Minion(minionCard);
		minion.Controller = MinionController.Create(this.BoardController, minion);
		this.BoardController.AddMinion(minion, position);
		return minion;
	}

	public void RemoveMinionFromBoard(Minion minion)
	{
		minion.Controller.DestroyController();
		this.Minions.Remove(minion);
		this.BoardController.RemoveMinion(minion);
		CardController cardController = CardController.Create(minion.Card, this.IsEnemy);
		minion.Card.Controller = cardController;
		this.Hand.Add(minion.Card);
		this.HandController.Add(cardController);
		cardController.StopAnimating();
		GameManager.Instance.GameUpdate();
	}

	public void EnableAuras(BaseCard card)
	{
		if (card.MinionAura != null)
		{
			AuraManager.Instance.AddMinionAura(card.MinionAura);
		}
		if (card.CardAura != null)
		{
			AuraManager.Instance.AddCardAura(card.CardAura);
		}
		if (card.HeroPowerAura != null)
		{
			AuraManager.Instance.AddHeroPowerAura(card.HeroPowerAura);
		}
		if (card.HeroAura != null)
		{
			AuraManager.Instance.AddHeroAura(card.HeroAura);
		}
	}

	public IEnumerator PlaySpell(SpellCard spellCard, Character target)
	{
		SpellPreCastEvent spellPreCastEvent = new SpellPreCastEvent
		{
			Player = this,
			Spell = spellCard,
			Target = target
		};
		yield return EventManager.Instance.OnSpellPreCast(spellPreCastEvent);
		target = spellPreCastEvent.Target;
		if (spellPreCastEvent.Status != PreStatus.Cancelled)
		{
			if (spellCard.TargetType == TargetType.NoTarget)
			{
				Debugger.LogPlayer(this, "casting spell " + spellCard.Name);
			}
			else
			{
				Debugger.LogPlayer(this, "casting spell " + spellCard.Name + " to " + target.GetName());
			}
			yield return spellCard.Cast(target);
			if (spellCard.Mechanics.HasMeditate())
			{
				this.AddMeditation(spellCard);
			}
		}
		else
		{
			Debugger.LogPlayer(this, "cancelled casting spell " + spellCard.Name);
		}
		yield return EventManager.Instance.OnSpellCasted(this, spellCard);
		yield return EventManager.Instance.OnCardPlayed(this, spellCard);
		this.PlayedSpells.Add(spellCard);
		GameManager.Instance.CurrentTurnPlayedSpells++;
		GameManager.Instance.GameUpdate();
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	public void PlaySecret(SpellCard secretCard)
	{
	}

	public void AddMeditation(BaseCard meditateCard)
	{
		this.Meditations.Add(meditateCard);
		SoundManager.Instance.Play("Game_Secret_Play");
		this.MeditateController.UpdateSprites();
		this.MeditateController.UpdateNumbers();
	}

	public void SetPresence(IDisposable presence, Presence type)
	{
		if (this.Presence != null)
		{
			this.Presence.Dispose();
		}
		this.Presence = presence;
		this.PresenceType = type;
		this.Hero.Controller.As<HeroController>().SetPresence(type);
	}

	public IEnumerator EquipWeapon(WeaponCard weaponCard, Character target)
	{
		Debugger.LogPlayer(this, "equipping weapon " + weaponCard.Name);
		WeaponPreEquipEvent weaponPreEquipEvent = new WeaponPreEquipEvent
		{
			Player = this,
			Weapon = weaponCard
		};
		yield return EventManager.Instance.OnWeaponPreEquip(weaponPreEquipEvent);
		yield return weaponCard.Mechanics.OnWeaponPreEquip.Fire(weaponPreEquipEvent);
		if (weaponPreEquipEvent.Status != PreStatus.Cancelled)
		{
			yield return this.DestroyWeapon();
			weaponCard.Player = this;
			this.Weapon = new Weapon(weaponCard);
			this.Weapon.Player = this;
			this.Weapon.Controller = WeaponController.Create(this, this.Weapon);
			weaponCard.Weapon = this.Weapon;
			SoundManager.Instance.Play("Game_Weapon_Equip");
			yield return this.Weapon.Mechanics.Battlecry.Fire(target);
			yield return EventManager.Instance.OnCardPlayed(this, weaponCard);
		}
		else
		{
			Debugger.LogPlayer(this, "cancelled equpping weapon " + weaponCard.Name);
		}
		GameManager.Instance.GameUpdate();
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	public IEnumerator DestroyWeapon()
	{
		if (this.Weapon != null)
		{
			Debugger.LogPlayer(this, "destroying weapon " + this.Weapon.Card.Name);
			yield return this.Weapon.Mechanics.Deathrattle.Fire(null);
			this.DestroyedWeapons.Add(this.Weapon.Card);
			this.Weapon.Controller.DestroyController();
			SoundManager.Instance.Play("Game_Weapon_Destroy");
			this.Weapon = null;
			GameManager.Instance.GameUpdate();
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public IEnumerator UseHeroPower(Character target)
	{
		Debugger.LogPlayer(this, "using hero power " + this.Hero.HeroPower.Name + " to " + target.GetName());
		this.Hero.HeroPower.Controller.IsQueued = false;
		yield return this.UseMana(this.Hero.HeroPower.CurrentCost);
		yield return this.Hero.HeroPower.Use(target);
		this.Hero.HeroPower.CurrentUses++;
		this.Hero.HeroPower.Controller.UpdateSprites();
		yield return EventManager.Instance.OnInspired(this.Hero, this.Hero.HeroPower);
		GameManager.Instance.GameUpdate();
		yield break;
	}

	public IEnumerator ReplaceHero(Minion origin, Hero newHero)
	{
		origin.Controller.As<MinionController>().ChangeRenderingOrder(1000);
		yield return origin.Controller.As<MinionController>().AnimateHeroReplace();
		this.Minions.Remove(origin);
		this.BoardController.RemoveMinion(origin);
		origin.Controller.DestroyController();
		newHero.CurrentHealth = newHero.BaseHealth;
		newHero.Player = this;
		newHero.HeroPower = this.Hero.HeroPower;
		HeroController newController = HeroController.Create(newHero, this.IsEnemy);
		newController.transform.ChangeParentAt(base.transform, new Vector3(0f, 7.5f, -10f));
		yield return newController.AnimateReplaceFromCenter();
		if (this.Presence != null)
		{
			newController.SetPresence(this.PresenceType);
		}
		this.Hero.Controller.DestroyController();
		this.Hero = newHero;
		this.Hero.Controller = newController;
		GameManager.Instance.GameUpdate();
		yield break;
	}

	public IEnumerator ReplaceHeroPower(BaseHeroPower newHeroPower)
	{
		BaseHeroPower oldHeroPower = this.Hero.HeroPower;
		this.Hero.HeroPower = newHeroPower;
		this.Hero.HeroPower.Controller = oldHeroPower.Controller;
		this.Hero.HeroPower.Controller.HeroPower = newHeroPower;
		this.Hero.HeroPower.Controller.UpdateSprites();
		this.Hero.HeroPower.Controller.UpdateNumbers();
		this.Hero.HeroPower.Controller.HoverController.HeroPower = newHeroPower;
		this.Hero.HeroPower.Controller.HoverController.UpdateSprites();
		this.Hero.HeroPower.Controller.HoverController.UpdateNumbers();
		yield return this.Hero.HeroPower.Controller.ReplaceAnimation();
		yield break;
	}

	public IEnumerator TakeControlOf(Minion minion)
	{
		if (minion.IsEnemyOf(this.Hero))
		{
			this.Enemy.BoardController.RemoveMinion(minion);
			this.Enemy.Minions.Remove(minion);
			minion.Controller.transform.parent = this.BoardController.transform;
			minion.Card.Player = this;
			minion.Player = this;
			minion.IsSleeping = false;
			minion.CurrentTurnAttacks = 0;
			this.BoardController.AddMinion(minion, this.Minions.Count);
			this.Minions.Add(minion);
			yield return new WaitForSeconds(0.25f);
		}
		yield break;
	}

	public void DiscoverCard(List<BaseCard> cards)
	{
		cards.Shuffle<BaseCard>();
		BaseCard[] cards2 = cards.Take(3).ToArray<BaseCard>();
		InterfaceManager.Instance.ShowCardSelection(new Func<BaseCard, IEnumerator>(this.AddCardToHand), cards2);
	}

	public void DiscoverCard(BaseCard firstCard, BaseCard secondCard, BaseCard thirdCard)
	{
		InterfaceManager.Instance.ShowCardSelection(new Func<BaseCard, IEnumerator>(this.AddCardToHand), new BaseCard[]
		{
			firstCard,
			secondCard,
			thirdCard
		});
	}

	public void ChooseOne(SpellCard firstCard, SpellCard secondCard)
	{
		InterfaceManager.Instance.ShowCardSelection((BaseCard c) => this.PlaySpell((SpellCard)c, null), new BaseCard[]
		{
			firstCard,
			secondCard
		});
	}

	public void Mulligan(params BaseCard[] cards)
	{
		InterfaceManager.Instance.ShowMulliganSelection(cards);
	}

	public void AddTurnMana(int quantity)
	{
		this.AvailableMana = Mathf.Min(this.AvailableMana + quantity, this.MaximumMana);
		this.ManaController.UpdateAll();
		GameManager.Instance.GameUpdate();
	}

	public void AddEmptyMana(int quantity)
	{
		this.TurnMana = Mathf.Min(this.TurnMana + quantity, this.MaximumMana);
		this.ManaController.UpdateAll();
	}

	public void AddOverloadedMana(int quantity)
	{
		this.NextOverloadedMana += quantity;
		this.ManaController.UpdateAll();
	}

	public IEnumerator UseMana(int quantity)
	{
		Debugger.LogPlayer(this, string.Concat(new object[]
		{
			"uses ",
			quantity,
			" mana (",
			this.AvailableMana,
			" -> ",
			this.AvailableMana - quantity,
			")"
		}));
		this.AvailableMana -= quantity;
		this.UsedMana += quantity;
		yield return EventManager.Instance.OnManaSpent(this, quantity);
		this.ManaController.UpdateAll();
		GameManager.Instance.GameUpdate();
		yield break;
	}

	public void RefillMana()
	{
		this.AvailableMana = Mathf.Clamp(this.TurnMana - this.CurrentOverloadedMana, 0, 10);
		this.ManaController.UpdateAll();
	}

	public IEnumerator AddCardToHand(BaseCard card)
	{
		card.Player = this;
		if (this.Hand.Count < this.MaxCardsInHand)
		{
			CardController cardController = CardController.Create(card, this.IsEnemy);
			card.Controller = cardController;
			this.Hand.Add(card);
			this.HandController.Add(cardController);
			if (card.HasHeld)
			{
				this.EnableAuras(card);
			}
			cardController.StopAnimating();
		}
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		yield break;
	}

	public void AddCardToDeck(BaseCard card)
	{
		int index = RNG.RandomInteger(0, this.Deck.Count);
		this.AddCardToDeck(card, index);
	}

	public void AddCardToDeck(BaseCard card, int index)
	{
		card.Player = this;
		if (this.Deck.Count < this.MaxCardsInDeck)
		{
			this.Deck.Insert(index, card);
		}
		GameManager.Instance.GameUpdate();
	}

	public void RemoveCardFromHand(BaseCard card)
	{
		if (this.Hand.Contains(card))
		{
			this.Hand.Remove(card);
			this.HandController.Remove(card.Controller);
			card.Controller.DestroyController();
		}
		GameManager.Instance.GameUpdate();
	}

	public void RemoveCardFromDeck(BaseCard card)
	{
		if (this.Deck.Contains(card))
		{
			this.Deck.Remove(card);
		}
		GameManager.Instance.GameUpdate();
	}

	public IEnumerator Draw(Func<BaseCard, IEnumerator> action = null)
	{
		yield return this.InternalDraw(true, action);
		yield break;
	}

	public IEnumerator Draw(int draws, Func<BaseCard, IEnumerator> action = null)
	{
		yield return this.InternalDraw(draws, true, action);
		yield break;
	}

	public IEnumerator InstantDraw(int draws, Func<BaseCard, IEnumerator> action = null)
	{
		yield return this.InternalDraw(draws, false, action);
		yield break;
	}

	private IEnumerator InternalDraw(int draws, bool animate, Func<BaseCard, IEnumerator> action)
	{
		for (int i = 0; i < draws; i++)
		{
			yield return this.InternalDraw(animate, action);
		}
		yield break;
	}

	private IEnumerator InternalDraw(bool animate, Func<BaseCard, IEnumerator> action)
	{
		if (this.Deck.Count > 0)
		{
			yield return this.InternalDrawFromDeck(this.Deck[0], animate, action);
		}
		else
		{
			this.Fatigue++;
			if (this.IsSelf())
			{
				yield return EffectsManager.Instance.ShowSelfFatigue(this.Fatigue);
			}
			else
			{
				yield return EffectsManager.Instance.ShowEnemyFatigue(this.Fatigue);
			}
			yield return this.Hero.Damage(null, this.Fatigue);
			yield return this.Hero.CheckDeath();
		}
		yield break;
	}

	public IEnumerator DrawFromDeck(BaseCard card, Func<BaseCard, IEnumerator> action = null)
	{
		yield return this.InternalDrawFromDeck(card, true, action);
		yield break;
	}

	public IEnumerator InstantDrawFromDeck(BaseCard card, Func<BaseCard, IEnumerator> action = null)
	{
		yield return this.InternalDrawFromDeck(card, false, action);
		yield break;
	}

	private IEnumerator InternalDrawFromDeck(BaseCard card, bool animate, Func<BaseCard, IEnumerator> action)
	{
		if (this.Deck.Contains(card))
		{
			this.Deck.Remove(card);
			CardController cardController = CardController.Create(card, this.IsEnemy);
			card.Controller = cardController;
			if (this.Hand.Count < this.MaxCardsInHand)
			{
				this.Hand.Add(card);
				this.HandController.Add(cardController);
				this.LastDrawnCard = card;
				if (animate)
				{
					yield return cardController.DrawAnimation();
				}
				else
				{
					cardController.StopAnimating();
				}
				if (card.HasHeld)
				{
					this.EnableAuras(card);
				}
				yield return card.Mechanics.OnDrawn.Fire(card);
				yield return EventManager.Instance.OnCardDrawn(this, card);
				if (action != null)
				{
					yield return action(card);
				}
			}
			else
			{
				this.HandController.SetAsParentOf(cardController);
				yield return cardController.DrawDiscardAnimation();
			}
			ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
		}
		yield break;
	}

	public void MulliganDrawFromDeck(BaseCard card, SelectionCardController previousController)
	{
		this.Deck.Remove(card);
		this.Hand.Add(card);
		CardController cardController = CardController.Create(card, this.IsEnemy);
		cardController.Speed = 50f;
		card.Controller = cardController;
		this.HandController.Add(cardController);
		cardController.StopAnimating();
		cardController.transform.position = previousController.transform.position;
		this.LastDrawnCard = card;
		if (card.HasHeld)
		{
			this.EnableAuras(card);
		}
	}

	public void UpdateVisuals()
	{
		this.ResetVisuals();
		this.Hero.Controller.UpdateNumbers();
		this.Hero.Controller.UpdateSprites();
		this.ManaController.UpdateAll();
		if (this.HasWeapon())
		{
			this.Weapon.Controller.OpenTokenRenderer.enabled = this.IsCurrent();
			this.Weapon.Controller.ClosedTokenRenderer.enabled = !this.IsCurrent();
		}
		this.UpdateHeroGlow();
		this.UpdateWeaponGlow();
		this.UpdateHandGlows();
		this.UpdateMinionGlows();
		this.UpdateHeroPowerGlow();
		this.UpdateMeditations();
	}

	public void ResetVisuals()
	{
		this.Hero.Controller.SetGreenRenderer(false);
		this.Hero.HeroPower.Controller.SetGreenRenderer(false);
		foreach (BaseCard baseCard in this.Hand)
		{
			baseCard.Controller.SetGreenRenderer(false);
		}
		if (this.HasWeapon())
		{
			this.Weapon.Controller.SetGreenRenderer(false);
		}
	}

	private void UpdateHeroGlow()
	{
		if (!this.IsEnemy && this.IsCurrent() && ((this.HasWeapon() && this.Weapon.CurrentAttack > 0) || this.Hero.CurrentAttack > 0))
		{
			int currentTurnAttacks = this.Hero.CurrentTurnAttacks;
			if (currentTurnAttacks != 0)
			{
				if (currentTurnAttacks == 1)
				{
					if (this.HasWeapon() && this.Weapon.HasWindfury)
					{
						this.Hero.Controller.SetGreenRenderer(true);
					}
				}
			}
			else
			{
				this.Hero.Controller.SetGreenRenderer(true);
			}
		}
	}

	private void UpdateWeaponGlow()
	{
		if (this.IsCurrent() && this.HasWeapon())
		{
			this.Weapon.Controller.UpdateSprites();
			this.Weapon.Controller.UpdateNumbers();
		}
	}

	private void UpdateHandGlows()
	{
		if (!this.IsEnemy)
		{
			foreach (BaseCard baseCard in this.Hand)
			{
				baseCard.Controller.UpdateNumbers();
				if (this.IsCurrent() && baseCard.CurrentCost <= this.AvailableMana)
				{
					CardType cardType = baseCard.GetCardType();
					if (cardType != CardType.Minion)
					{
						if (cardType != CardType.Spell)
						{
							if (cardType == CardType.Weapon)
							{
								if (this.CanPlayWeapons)
								{
									baseCard.Controller.SetGreenRenderer(true);
								}
							}
						}
						else if (this.CanPlaySpells && baseCard.As<SpellCard>().CanCast())
						{
							baseCard.Controller.SetGreenRenderer(true);
						}
					}
					else if (this.CanPlayMinions && this.Minions.Count < 7)
					{
						baseCard.Controller.SetGreenRenderer(true);
					}
				}
			}
		}
	}

	private void UpdateMinionGlows()
	{
		foreach (Minion minion in this.Minions)
		{
			minion.Controller.UpdateNumbers();
			minion.Controller.UpdateSprites();
			if (!this.IsEnemy && this.IsCurrent() && minion.CanAttack())
			{
				minion.Controller.SetGreenRenderer(true);
			}
			else
			{
				minion.Controller.SetGreenRenderer(false);
			}
		}
	}

	private void UpdateHeroPowerGlow()
	{
		if (!this.IsEnemy && this.IsCurrent() && this.Hero.HeroPower.IsAvailable() && this.Hero.HeroPower.CanUse())
		{
			this.Hero.HeroPower.Controller.SetGreenRenderer(true);
		}
	}

	private void UpdateMeditations()
	{
		this.MeditateController.UpdateSprites();
		this.MeditateController.UpdateNumbers();
	}

	public List<Character> GetAllCharacters()
	{
		List<Character> first = new List<Character>
		{
			this.Hero
		};
		return first.Concat((from m in this.Minions
		where m.IsAlive()
		select m).Cast<Character>()).ToList<Character>();
	}

	public int GetSpellPower()
	{
		int num = 0;
		foreach (Minion minion in this.Minions)
		{
			num += minion.SpellPower;
		}
		return num;
	}

	public int GetUsedMana()
	{
		return this.TurnMana - this.CurrentOverloadedMana - this.AvailableMana;
	}

	public bool IsSelf()
	{
		return !this.IsEnemy;
	}

	public bool IsCurrent()
	{
		return this == GameManager.Instance.CurrentPlayer;
	}

	public bool CanDoSomething()
	{
		foreach (Minion minion in this.Minions)
		{
			if (minion.CanAttack())
			{
				return true;
			}
		}
		if (this.Hero.HeroPower.IsAvailable())
		{
			return true;
		}
		if (this.Hero.CanAttack())
		{
			return true;
		}
		foreach (BaseCard baseCard in this.Hand)
		{
			if (this.AvailableMana >= baseCard.CurrentCost)
			{
				if (baseCard.GetCardType() != CardType.Minion)
				{
					return true;
				}
				if (this.Minions.Count < 7)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool HasWeapon()
	{
		return this.Weapon != null;
	}

	public bool HasMinions()
	{
		return this.Minions.Count > 0;
	}

	public bool HasTauntMinions()
	{
		foreach (Minion minion in this.Minions)
		{
			if (minion.HasTaunt)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasManaToPlay(BaseCard card)
	{
		return this.AvailableMana >= card.CurrentCost;
	}

	public Player Enemy;

	public Hero Hero;

	public Weapon Weapon;

	public List<BaseCard> Hand = new List<BaseCard>();

	public List<BaseCard> Deck = new List<BaseCard>();

	public List<Minion> Minions = new List<Minion>(7);

	public List<SpellCard> Secrets = new List<SpellCard>();

	public List<BaseCard> Meditations = new List<BaseCard>();

	public IDisposable Presence;

	public Presence PresenceType;

	public ManaController ManaController;

	public HandController HandController;

	public BoardController BoardController;

	public MeditateController MeditateController;

	public List<SpellCard> PlayedSpells = new List<SpellCard>();

	public List<MinionCard> PlayedMinions = new List<MinionCard>();

	public List<MinionCard> SummonedMinions = new List<MinionCard>();

	public List<MinionCard> DeadMinions = new List<MinionCard>();

	public List<WeaponCard> DestroyedWeapons = new List<WeaponCard>();

	public List<BaseCard> DiscardedCards = new List<BaseCard>();

	public BaseCard LastDrawnCard;

	public int MaxCardsInHand = 10;

	public int MaxCardsInDeck = 60;

	public bool CanPlayMinions = true;

	public bool CanPlaySpells = true;

	public bool CanPlayWeapons = true;

	public bool CanHeroPower = true;

	public int Fatigue;

	public int MaximumMana = 10;

	public int TurnMana;

	public int UsedMana;

	public int AvailableMana;

	public int CurrentOverloadedMana;

	public int NextOverloadedMana;

	public bool IsEnemy;

	public bool IsSelectingCard;
}
