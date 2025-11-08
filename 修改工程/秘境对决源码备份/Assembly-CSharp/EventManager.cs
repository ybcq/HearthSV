using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class EventManager
{
	private EventManager()
	{
	}

	public static EventManager Instance
	{
		get
		{
			if (EventManager._instance == null)
			{
				EventManager._instance = new EventManager();
			}
			return EventManager._instance;
		}
	}

	public IEnumerator OnMinionPlayed(Player player, Minion minion)
	{
		MinionPlayedEvent minionPlayedEvent = new MinionPlayedEvent
		{
			Player = player,
			Minion = minion
		};
		yield return this.MinionPlayedHandler.Fire(minionPlayedEvent);
		yield return this.FireOnMinions("OnMinionPlayed", minionPlayedEvent);
		yield return this.FireOnWeapons("OnMinionPlayed", minionPlayedEvent);
		yield break;
	}

	public IEnumerator OnMinionSummoned(Player player, Minion minion)
	{
		MinionSummonedEvent minionSummonedEvent = new MinionSummonedEvent
		{
			Player = player,
			Minion = minion
		};
		yield return this.MinionSummonedHandler.Fire(minionSummonedEvent);
		yield return this.FireOnMinions("OnMinionSummoned", minionSummonedEvent);
		yield return this.FireOnWeapons("OnMinionSummoned", minionSummonedEvent);
		yield break;
	}

	public IEnumerator OnMinionFrozen(Minion minion, Character freezingMinion)
	{
		MinionFrozenEvent minionFrozenEvent = new MinionFrozenEvent
		{
			Minion = minion,
			FreezingCharacter = freezingMinion
		};
		yield return this.MinionFrozenHandler.Fire(minionFrozenEvent);
		yield return this.FireOnMinions("OnMinionFrozen", minionFrozenEvent);
		yield return this.FireOnWeapons("OnMinionFrozen", minionFrozenEvent);
		yield break;
	}

	public IEnumerator OnMinionEnraged(Minion minion, Character enragedMinion)
	{
		MinionEnragedEvent minionEnragedEvent = new MinionEnragedEvent
		{
			Minion = minion,
			EnragedCharacter = enragedMinion
		};
		yield return this.MinionEnragedHandler.Fire(minionEnragedEvent);
		yield return this.FireOnMinions("OnMinionEnraged", minionEnragedEvent);
		yield return this.FireOnWeapons("OnMinionEnraged", minionEnragedEvent);
		yield break;
	}

	public IEnumerator OnMinionPoisoned(Minion minion, Character attacker)
	{
		MinionPoisonedEvent minionPoisonedEvent = new MinionPoisonedEvent
		{
			Minion = minion,
			Attacker = attacker
		};
		yield return this.MinionPoisonedHandler.Fire(minionPoisonedEvent);
		yield return this.FireOnMinions("OnMinionPoisoned", minionPoisonedEvent);
		yield return this.FireOnWeapons("OnMinionPoisoned", minionPoisonedEvent);
		yield break;
	}

	public IEnumerator OnMinionPreAttack(MinionPreAttackEvent minionPreAttackEvent)
	{
		yield return this.MinionPreAttackHandler.Fire(minionPreAttackEvent);
		yield return this.FireOnMinions("OnMinionPreAttack", minionPreAttackEvent);
		yield return this.FireOnWeapons("OnMinionPreAttack", minionPreAttackEvent);
		yield break;
	}

	public IEnumerator OnMinionAttacked(MinionAttackedEvent minionAttackedEvent)
	{
		yield return this.MinionAttackedHandler.Fire(minionAttackedEvent);
		yield return this.FireOnMinions("OnMinionAttacked", minionAttackedEvent);
		yield return this.FireOnWeapons("OnMinionAttacked", minionAttackedEvent);
		yield break;
	}

	public IEnumerator OnMinionPreDamage(MinionPreDamageEvent minionPreDamageEvent)
	{
		yield return this.MinionPreDamageHandler.Fire(minionPreDamageEvent);
		yield return this.FireOnMinions("OnMinionPreDamage", minionPreDamageEvent);
		yield return this.FireOnWeapons("OnMinionPreDamage", minionPreDamageEvent);
		CharacterPreDamageEvent characterPreDamageEvent = new CharacterPreDamageEvent(minionPreDamageEvent);
		yield return this.CharacterPreDamageHandler.Fire(characterPreDamageEvent);
		yield return this.FireOnMinions("OnCharacterPreDamage", characterPreDamageEvent);
		yield return this.FireOnWeapons("OnCharacterPreDamage", characterPreDamageEvent);
		minionPreDamageEvent.Status = characterPreDamageEvent.Status;
		minionPreDamageEvent.DamageAmount = characterPreDamageEvent.DamageAmount;
		yield break;
	}

	public IEnumerator OnMinionDamaged(MinionDamagedEvent minionDamagedEvent)
	{
		yield return this.MinionDamagedHandler.Fire(minionDamagedEvent);
		yield return this.FireOnMinions("OnMinionDamaged", minionDamagedEvent);
		yield return this.FireOnWeapons("OnMinionDamaged", minionDamagedEvent);
		yield break;
	}

	public IEnumerator OnMinionPreHeal(MinionPreHealEvent minionPreHealEvent)
	{
		yield return this.MinionPreHealHandler.Fire(minionPreHealEvent);
		yield return this.FireOnMinions("OnMinionPreHeal", minionPreHealEvent);
		yield return this.FireOnWeapons("OnMinionPreHeal", minionPreHealEvent);
		CharacterPreHealEvent characterPreHealEvent = new CharacterPreHealEvent(minionPreHealEvent);
		yield return this.CharacterPreHealHandler.Fire(characterPreHealEvent);
		yield return this.FireOnMinions("OnCharacterPreHeal", characterPreHealEvent);
		yield return this.FireOnWeapons("OnCharacterPreHeal", characterPreHealEvent);
		minionPreHealEvent.Status = characterPreHealEvent.Status;
		minionPreHealEvent.HealAmount = characterPreHealEvent.HealAmount;
		yield break;
	}

	public IEnumerator OnMinionHealed(Minion minion, int healAmount)
	{
		MinionHealedEvent minionHealedEvent = new MinionHealedEvent
		{
			Minion = minion,
			HealAmount = healAmount
		};
		yield return this.MinionHealedHandler.Fire(minionHealedEvent);
		yield return this.FireOnMinions("OnMinionHealed", minionHealedEvent);
		yield return this.FireOnWeapons("OnMinionHealed", minionHealedEvent);
		yield break;
	}

	public IEnumerator OnMinionDied(Minion minion)
	{
		MinionDiedEvent minionDiedEvent = new MinionDiedEvent
		{
			Minion = minion
		};
		yield return this.MinionDiedHandler.Fire(minionDiedEvent);
		yield return this.FireOnMinions("OnMinionDied", minionDiedEvent);
		yield return this.FireOnWeapons("OnMinionDied", minionDiedEvent);
		yield break;
	}

	public IEnumerator OnMinionBuffAttack(Minion minion, int delta)
	{
		MinionBuffEvent minionBuffEvent = new MinionBuffEvent
		{
			Minion = minion,
			Delta = delta
		};
		yield return this.MinionBuffAttackHandler.Fire(minionBuffEvent);
		yield return this.FireOnMinions("OnMinionBuffAttack", minionBuffEvent);
		yield return this.FireOnWeapons("OnMinionBuffAttack", minionBuffEvent);
		yield break;
	}

	public IEnumerator OnMinionDebuffAttack(Minion minion, int delta)
	{
		MinionBuffEvent minionBuffEvent = new MinionBuffEvent
		{
			Minion = minion,
			Delta = delta
		};
		yield return this.MinionDebuffAttackHandler.Fire(minionBuffEvent);
		yield return this.FireOnMinions("OnMinionDebuffAttack", minionBuffEvent);
		yield return this.FireOnWeapons("OnMinionDebuffAttack", minionBuffEvent);
		yield break;
	}

	public IEnumerator OnMinionEvade(Minion minion, Character attacker)
	{
		MinionEvadeEvent minionEvadeEvent = new MinionEvadeEvent
		{
			Minion = minion,
			Attacker = attacker
		};
		yield return this.MinionEvadeHandler.Fire(minionEvadeEvent);
		yield return this.FireOnMinions("OnMinionEvade", minionEvadeEvent);
		yield return this.FireOnWeapons("OnMinionEvade", minionEvadeEvent);
		yield break;
	}

	public IEnumerator OnHeroPreAttack(HeroPreAttackEvent heroPreAttackEvent)
	{
		yield return this.HeroPreAttackHandler.Fire(heroPreAttackEvent);
		yield return this.FireOnMinions("OnHeroPreAttack", heroPreAttackEvent);
		yield return this.FireOnWeapons("OnHeroPreAttack", heroPreAttackEvent);
		yield break;
	}

	public IEnumerator OnHeroAttacked(Hero hero, Character target)
	{
		HeroAttackedEvent heroAttackedEvent = new HeroAttackedEvent
		{
			Hero = hero,
			Target = target
		};
		yield return this.HeroAttackedHandler.Fire(heroAttackedEvent);
		yield return this.FireOnMinions("OnHeroAttacked", heroAttackedEvent);
		yield return this.FireOnWeapons("OnHeroAttacked", heroAttackedEvent);
		yield break;
	}

	public IEnumerator OnHeroPreDamage(HeroPreDamageEvent heroPreDamageEvent)
	{
		yield return this.HeroPreDamageHandler.Fire(heroPreDamageEvent);
		yield return this.FireOnMinions("OnHeroPreDamage", heroPreDamageEvent);
		yield return this.FireOnWeapons("OnHeroPreDamage", heroPreDamageEvent);
		CharacterPreDamageEvent characterPreDamageEvent = new CharacterPreDamageEvent(heroPreDamageEvent);
		yield return this.CharacterPreDamageHandler.Fire(characterPreDamageEvent);
		yield return this.FireOnMinions("OnCharacterPreDamage", characterPreDamageEvent);
		yield return this.FireOnWeapons("OnCharacterPreDamage", characterPreDamageEvent);
		heroPreDamageEvent.Status = characterPreDamageEvent.Status;
		heroPreDamageEvent.DamageAmount = characterPreDamageEvent.DamageAmount;
		yield break;
	}

	public IEnumerator OnHeroDamaged(Hero hero, Character attacker, int damageAmount)
	{
		HeroDamagedEvent heroDamagedEvent = new HeroDamagedEvent
		{
			Hero = hero,
			Attacker = attacker,
			DamageAmount = damageAmount
		};
		yield return this.HeroDamagedHandler.Fire(heroDamagedEvent);
		yield return this.FireOnMinions("OnHeroDamaged", heroDamagedEvent);
		yield return this.FireOnWeapons("OnHeroDamaged", heroDamagedEvent);
		yield break;
	}

	public IEnumerator OnHeroPreHeal(HeroPreHealEvent heroPreHealEvent)
	{
		yield return this.HeroPreHealHandler.Fire(heroPreHealEvent);
		yield return this.FireOnMinions("OnHeroPreHeal", heroPreHealEvent);
		yield return this.FireOnWeapons("OnHeroPreHeal", heroPreHealEvent);
		CharacterPreHealEvent characterPreHealEvent = new CharacterPreHealEvent(heroPreHealEvent);
		yield return this.CharacterPreHealHandler.Fire(characterPreHealEvent);
		yield return this.FireOnMinions("OnCharacterPreHeal", characterPreHealEvent);
		yield return this.FireOnWeapons("OnCharacterPreHeal", characterPreHealEvent);
		heroPreHealEvent.Status = characterPreHealEvent.Status;
		heroPreHealEvent.HealAmount = characterPreHealEvent.HealAmount;
		yield break;
	}

	public IEnumerator OnHeroHealed(Hero hero, int healAmount)
	{
		HeroHealedEvent heroHealedEvent = new HeroHealedEvent
		{
			Hero = hero,
			HealAmount = healAmount
		};
		yield return this.HeroHealedHandler.Fire(heroHealedEvent);
		yield return this.FireOnMinions("OnHeroHealed", heroHealedEvent);
		yield return this.FireOnWeapons("OnHeroHealed", heroHealedEvent);
		yield break;
	}

	public IEnumerator OnHeroGainedArmor(Hero hero, int armorAmount)
	{
		HeroGainedArmorEvent heroGainedArmorEvent = new HeroGainedArmorEvent
		{
			Hero = hero,
			ArmorAmount = armorAmount
		};
		yield return this.HeroGainedArmorHandler.Fire(heroGainedArmorEvent);
		yield return this.FireOnMinions("OnHeroGainedArmor", heroGainedArmorEvent);
		yield return this.FireOnWeapons("OnHeroGainedArmor", heroGainedArmorEvent);
		yield break;
	}

	public IEnumerator OnHeroBuffAttack(Hero hero, int delta)
	{
		HeroBuffEvent heroBuffEvent = new HeroBuffEvent
		{
			Hero = hero,
			Delta = delta
		};
		yield return this.HeroBuffAttackHandler.Fire(heroBuffEvent);
		yield return this.FireOnMinions("OnHeroBuffAttack", heroBuffEvent);
		yield return this.FireOnWeapons("OnHeroBuffAttack", heroBuffEvent);
		yield break;
	}

	public IEnumerator OnHeroDebuffAttack(Hero hero, int delta)
	{
		HeroBuffEvent heroBuffEvent = new HeroBuffEvent
		{
			Hero = hero,
			Delta = delta
		};
		yield return this.HeroDebuffAttackHandler.Fire(heroBuffEvent);
		yield return this.FireOnMinions("OnHeroDebuffAttack", heroBuffEvent);
		yield return this.FireOnWeapons("OnHeroDebuffAttack", heroBuffEvent);
		yield break;
	}

	public IEnumerator OnHeroEvade(Hero hero, Character attacker)
	{
		HeroEvadeEvent heroEvadeEvent = new HeroEvadeEvent
		{
			Hero = hero,
			Attacker = attacker
		};
		yield return this.HeroEvadeHandler.Fire(heroEvadeEvent);
		yield return this.FireOnMinions("OnHeroEvade", heroEvadeEvent);
		yield return this.FireOnWeapons("OnHeroEvade", heroEvadeEvent);
		yield break;
	}

	public IEnumerator OnSpellPreCast(SpellPreCastEvent spellPreCastEvent)
	{
		yield return this.SpellPreCastHandler.Fire(spellPreCastEvent);
		yield return this.FireOnMinions("OnSpellPreCast", spellPreCastEvent);
		yield return this.FireOnWeapons("OnSpellPreCast", spellPreCastEvent);
		yield break;
	}

	public IEnumerator OnSpellCasted(Player player, SpellCard spell)
	{
		SpellCastedEvent spellCastedEvent = new SpellCastedEvent
		{
			Player = player,
			Spell = spell
		};
		yield return this.SpellCastedHandler.Fire(spellCastedEvent);
		yield return this.FireOnMinions("OnSpellCasted", spellCastedEvent);
		yield return this.FireOnWeapons("OnSpellCasted", spellCastedEvent);
		yield break;
	}

	public IEnumerator OnSecretPlayed(Player player, SpellCard secret)
	{
		SecretPlayedEvent secretPlayedEvent = new SecretPlayedEvent
		{
			Player = player,
			Secret = secret
		};
		yield return this.SecretPlayedHandler.Fire(secretPlayedEvent);
		yield return this.FireOnMinions("OnSecretPlayed", secretPlayedEvent);
		yield return this.FireOnWeapons("OnSecretPlayed", secretPlayedEvent);
		yield break;
	}

	public IEnumerator OnSecretRevealed(Player player, SpellCard secret)
	{
		SecretRevealedEvent secretRevealedEvent = new SecretRevealedEvent
		{
			Player = player,
			Secret = secret
		};
		yield return this.SecretRevealedHandler.Fire(secretRevealedEvent);
		yield return this.FireOnMinions("OnSecretRevealed", secretRevealedEvent);
		yield return this.FireOnWeapons("OnSecretRevealed", secretRevealedEvent);
		yield break;
	}

	public IEnumerator OnCardPlayed(Player player, BaseCard card)
	{
		CardPlayedEvent cardPlayedEvent = new CardPlayedEvent
		{
			Player = player,
			Card = card
		};
		ActionQueue.Add(() => this.CardPlayedHandler.Fire(cardPlayedEvent));
		yield return this.FireOnMinions("OnCardPlayed", cardPlayedEvent);
		yield return this.FireOnWeapons("OnCardPlayed", cardPlayedEvent);
		if (card.Overload > 0)
		{
			yield return this.FireOnMinions("OnOverloadedCardPlayed", cardPlayedEvent);
			yield return this.FireOnWeapons("OnOverloadedCardPlayed", cardPlayedEvent);
		}
		if (card.GetType() == typeof(MinionCard) && card.As<MinionCard>().Mechanics.HasBattlecry())
		{
			yield return this.FireOnMinions("OnBattlecryCardPlayed", cardPlayedEvent);
			yield return this.FireOnWeapons("OnBattlecryCardPlayed", cardPlayedEvent);
		}
		yield break;
	}

	public IEnumerator OnCardDrawn(Player player, BaseCard card)
	{
		CardDrawnEvent cardDrawnEvent = new CardDrawnEvent
		{
			Player = player,
			Card = card
		};
		yield return this.CardDrawnHandler.Fire(cardDrawnEvent);
		yield return this.FireOnMinions("OnCardDrawn", cardDrawnEvent);
		yield return this.FireOnWeapons("OnCardDrawn", cardDrawnEvent);
		yield return this.FireOnHand("OnHandCardDrawn", cardDrawnEvent);
		yield break;
	}

	public IEnumerator OnCardDiscarded(Player player, BaseCard card)
	{
		CardDiscardedEvent cardDiscardedEvent = new CardDiscardedEvent
		{
			Player = player,
			Card = card
		};
		yield return this.CardDiscardedHandler.Fire(cardDiscardedEvent);
		yield return this.FireOnMinions("OnCardDiscarded", cardDiscardedEvent);
		yield return this.FireOnWeapons("OnCardDiscarded", cardDiscardedEvent);
		yield break;
	}

	public IEnumerator OnInspired(Hero hero, BaseHeroPower heroPower)
	{
		InspireEvent inspireEvent = new InspireEvent
		{
			Hero = hero,
			HeroPower = heroPower
		};
		yield return this.InspireHandler.Fire(inspireEvent);
		yield return this.FireOnMinions("OnInspired", inspireEvent);
		yield return this.FireOnWeapons("OnInspired", inspireEvent);
		yield break;
	}

	public IEnumerator OnManaSpent(Player player, int manaAmount)
	{
		ManaSpentEvent manaSpentEvent = new ManaSpentEvent
		{
			Player = player,
			ManaAmount = manaAmount
		};
		yield return this.ManaSpentHandler.Fire(manaSpentEvent);
		yield return this.FireOnMinions("OnManaSpent", manaSpentEvent);
		yield return this.FireOnWeapons("OnManaSpent", manaSpentEvent);
		yield break;
	}

	public IEnumerator OnWeaponPreEquip(WeaponPreEquipEvent weaponPreEquipEvent)
	{
		yield return this.WeaponPreEquipHandler.Fire(weaponPreEquipEvent);
		yield return this.FireOnMinions("OnWeaponPreEquip", weaponPreEquipEvent);
		yield return this.FireOnWeapons("OnWeaponPreEquip", weaponPreEquipEvent);
		yield break;
	}

	public IEnumerator OnWeaponEquipped(Player player, WeaponCard weapon)
	{
		WeaponEquippedEvent weaponEquippedEvent = new WeaponEquippedEvent
		{
			Player = player,
			Weapon = weapon
		};
		yield return this.WeaponEquippedHandler.Fire(weaponEquippedEvent);
		yield return this.FireOnMinions("OnWeaponEquipped", weaponEquippedEvent);
		yield return this.FireOnWeapons("OnWeaponEquipped", weaponEquippedEvent);
		yield break;
	}

	public IEnumerator OnGameStart()
	{
		yield return this.GameStartHandler.Fire(null);
		yield return this.FireOnAll("OnGameStart", null);
		yield break;
	}

	public IEnumerator OnTurnStart(Player player)
	{
		TurnEvent turnStartEvent = new TurnEvent
		{
			Player = player
		};
		yield return this.TurnStartHandler.Fire(turnStartEvent);
		yield return this.FireOnMinions("OnTurnStart", turnStartEvent);
		yield return this.FireOnWeapons("OnTurnStart", turnStartEvent);
		yield return this.FireOnHand("OnHandTurnStart", turnStartEvent);
		yield break;
	}

	public IEnumerator OnTurnEnd(Player player)
	{
		TurnEvent turnEndEvent = new TurnEvent
		{
			Player = player
		};
		yield return this.TurnEndHandler.Fire(turnEndEvent);
		yield return this.FireOnMinions("OnTurnEnd", turnEndEvent);
		yield return this.FireOnWeapons("OnTurnEnd", turnEndEvent);
		yield return this.FireOnHand("OnHandTurnEnd", turnEndEvent);
		yield break;
	}

	private IEnumerator FireOnMinions(string subject, object value)
	{
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion != null)
			{
				yield return minion.Mechanics.GlobalSubjects[subject].Fire(value);
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		yield break;
		yield break;
	}

	private IEnumerator FireOnWeapons(string subject, object value)
	{
		if (GameManager.Instance.SelfPlayer.HasWeapon())
		{
			yield return GameManager.Instance.SelfPlayer.Weapon.Mechanics.GlobalSubjects[subject].Fire(value);
		}
		if (GameManager.Instance.EnemyPlayer.HasWeapon())
		{
			yield return GameManager.Instance.EnemyPlayer.Weapon.Mechanics.GlobalSubjects[subject].Fire(value);
		}
		yield break;
	}

	private IEnumerator FireOnHand(string subject, object value)
	{
		foreach (BaseCard handCard in GameManager.Instance.GetAllHandCards())
		{
			BaseCard card = handCard;
			yield return card.Mechanics.HandSubjects[subject].Fire(value);
		}
		yield break;
	}

	private IEnumerator FireOnAll(string subject, object value)
	{
		List<BaseCard> handCards = GameManager.Instance.GetAllHandCards();
		List<BaseCard> deckCards = GameManager.Instance.GetAllDeckCards();
		foreach (BaseCard card in handCards.Concat(deckCards))
		{
			BaseCard scopedCard = card;
			yield return scopedCard.Mechanics.GlobalSubjects[subject].Fire(value);
		}
		yield break;
	}

	public void Reset()
	{
		FieldInfo[] fields = typeof(EventManager).GetFields();
		foreach (FieldInfo fieldInfo in fields)
		{
			object value = fieldInfo.GetValue(this);
			if (!(value is EventManager))
			{
				MethodInfo method = value.GetType().GetMethod("DisposeAll");
				method.Invoke(value, null);
			}
		}
	}

	private static EventManager _instance;

	public EventHolder<object> GameStartHandler = new EventHolder<object>();

	public EventHolder<CharacterPreHealEvent> CharacterPreHealHandler = new EventHolder<CharacterPreHealEvent>();

	public EventHolder<CharacterHealedEvent> CharacterHealedHandler = new EventHolder<CharacterHealedEvent>();

	public EventHolder<CharacterPreDamageEvent> CharacterPreDamageHandler = new EventHolder<CharacterPreDamageEvent>();

	public EventHolder<CharacterDamagedEvent> CharacterDamagedHandler = new EventHolder<CharacterDamagedEvent>();

	public EventHolder<MinionPlayedEvent> MinionPlayedHandler = new EventHolder<MinionPlayedEvent>();

	public EventHolder<MinionSummonedEvent> MinionSummonedHandler = new EventHolder<MinionSummonedEvent>();

	public EventHolder<MinionPreAttackEvent> MinionPreAttackHandler = new EventHolder<MinionPreAttackEvent>();

	public EventHolder<MinionAttackedEvent> MinionAttackedHandler = new EventHolder<MinionAttackedEvent>();

	public EventHolder<MinionPreDamageEvent> MinionPreDamageHandler = new EventHolder<MinionPreDamageEvent>();

	public EventHolder<MinionDamagedEvent> MinionDamagedHandler = new EventHolder<MinionDamagedEvent>();

	public EventHolder<MinionPreHealEvent> MinionPreHealHandler = new EventHolder<MinionPreHealEvent>();

	public EventHolder<MinionHealedEvent> MinionHealedHandler = new EventHolder<MinionHealedEvent>();

	public EventHolder<MinionDiedEvent> MinionDiedHandler = new EventHolder<MinionDiedEvent>();

	public EventHolder<MinionPoisonedEvent> MinionPoisonedHandler = new EventHolder<MinionPoisonedEvent>();

	public EventHolder<MinionEnragedEvent> MinionEnragedHandler = new EventHolder<MinionEnragedEvent>();

	public EventHolder<MinionFrozenEvent> MinionFrozenHandler = new EventHolder<MinionFrozenEvent>();

	public EventHolder<MinionBuffEvent> MinionBuffAttackHandler = new EventHolder<MinionBuffEvent>();

	public EventHolder<MinionBuffEvent> MinionDebuffAttackHandler = new EventHolder<MinionBuffEvent>();

	public EventHolder<MinionEvadeEvent> MinionEvadeHandler = new EventHolder<MinionEvadeEvent>();

	public EventHolder<HeroPreAttackEvent> HeroPreAttackHandler = new EventHolder<HeroPreAttackEvent>();

	public EventHolder<HeroAttackedEvent> HeroAttackedHandler = new EventHolder<HeroAttackedEvent>();

	public EventHolder<HeroPreDamageEvent> HeroPreDamageHandler = new EventHolder<HeroPreDamageEvent>();

	public EventHolder<HeroDamagedEvent> HeroDamagedHandler = new EventHolder<HeroDamagedEvent>();

	public EventHolder<HeroPreHealEvent> HeroPreHealHandler = new EventHolder<HeroPreHealEvent>();

	public EventHolder<HeroHealedEvent> HeroHealedHandler = new EventHolder<HeroHealedEvent>();

	public EventHolder<HeroGainedArmorEvent> HeroGainedArmorHandler = new EventHolder<HeroGainedArmorEvent>();

	public EventHolder<HeroBuffEvent> HeroBuffAttackHandler = new EventHolder<HeroBuffEvent>();

	public EventHolder<HeroBuffEvent> HeroDebuffAttackHandler = new EventHolder<HeroBuffEvent>();

	public EventHolder<HeroEvadeEvent> HeroEvadeHandler = new EventHolder<HeroEvadeEvent>();

	public EventHolder<WeaponPreEquipEvent> WeaponPreEquipHandler = new EventHolder<WeaponPreEquipEvent>();

	public EventHolder<WeaponEquippedEvent> WeaponEquippedHandler = new EventHolder<WeaponEquippedEvent>();

	public EventHolder<InspireEvent> InspireHandler = new EventHolder<InspireEvent>();

	public EventHolder<SpellPreCastEvent> SpellPreCastHandler = new EventHolder<SpellPreCastEvent>();

	public EventHolder<SpellCastedEvent> SpellCastedHandler = new EventHolder<SpellCastedEvent>();

	public EventHolder<CardPlayedEvent> CardPlayedHandler = new EventHolder<CardPlayedEvent>();

	public EventHolder<CardDrawnEvent> CardDrawnHandler = new EventHolder<CardDrawnEvent>();

	public EventHolder<CardDiscardedEvent> CardDiscardedHandler = new EventHolder<CardDiscardedEvent>();

	public EventHolder<SecretPlayedEvent> SecretPlayedHandler = new EventHolder<SecretPlayedEvent>();

	public EventHolder<SecretRevealedEvent> SecretRevealedHandler = new EventHolder<SecretRevealedEvent>();

	public EventHolder<ManaSpentEvent> ManaSpentHandler = new EventHolder<ManaSpentEvent>();

	public EventHolder<TurnEvent> TurnStartHandler = new EventHolder<TurnEvent>();

	public EventHolder<TurnEvent> TurnEndHandler = new EventHolder<TurnEvent>();
}
