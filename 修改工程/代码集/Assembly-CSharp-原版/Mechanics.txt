using System;
using System.Collections.Generic;

public class Mechanics
{
	public Mechanics()
	{
		this.SelfSubjects = new Dictionary<string, IFireable>
		{
			{
				"Battlecry",
				this.Battlecry
			},
			{
				"Deathrattle",
				this.Deathrattle
			},
			{
				"OnPreAttack",
				this.OnPreAttack
			},
			{
				"OnAttacked",
				this.OnAttacked
			},
			{
				"OnPreDamage",
				this.OnPreDamage
			},
			{
				"OnDamaged",
				this.OnDamaged
			},
			{
				"OnInspired",
				this.OnDamaged
			},
			{
				"OnTargetedBySpell",
				this.OnTargetedBySpell
			},
			{
				"OnEnraged",
				this.OnEnraged
			},
			{
				"OnDisenraged",
				this.OnEnraged
			},
			{
				"OnDrawn",
				this.OnEnraged
			},
			{
				"OnDiscarded",
				this.OnEnraged
			}
		};
		this.GlobalSubjects = new Dictionary<string, IFireable>
		{
			{
				"OnGameStart",
				this.OnGameStart
			},
			{
				"OnCardDrawn",
				this.OnCardDrawn
			},
			{
				"OnCardDiscarded",
				this.OnCardDiscarded
			},
			{
				"OnCardPlayed",
				this.OnCardPlayed
			},
			{
				"OnOverloadedCardPlayed",
				this.OnOverloadedCardPlayed
			},
			{
				"OnBattlecryCardPlayed",
				this.OnBattlecryCardPlayed
			},
			{
				"OnSpellPreCast",
				this.OnSpellPreCast
			},
			{
				"OnSpellCasted",
				this.OnSpellCasted
			},
			{
				"OnSecretPlayed",
				this.OnSecretPlayed
			},
			{
				"OnSecretRevealed",
				this.OnSecretRevealed
			},
			{
				"OnCharacterPreHeal",
				this.OnCharacterPreHeal
			},
			{
				"OnMinionPlayed",
				this.OnMinionPlayed
			},
			{
				"OnMinionSummoned",
				this.OnMinionSummoned
			},
			{
				"OnMinionPreAttack",
				this.OnMinionPreAttack
			},
			{
				"OnMinionAttacked",
				this.OnMinionAttacked
			},
			{
				"OnMinionPreDamage",
				this.OnMinionPreDamage
			},
			{
				"OnMinionDamaged",
				this.OnMinionDamaged
			},
			{
				"OnMinionPreHeal",
				this.OnMinionPreHeal
			},
			{
				"OnMinionHealed",
				this.OnMinionHealed
			},
			{
				"OnMinionDied",
				this.OnMinionDied
			},
			{
				"OnMinionPoisoned",
				this.OnMinionPoisoned
			},
			{
				"OnMinionFrozen",
				this.OnMinionFrozen
			},
			{
				"OnMinionEnraged",
				this.OnMinionEnraged
			},
			{
				"OnMinionBuffAttack",
				this.OnMinionBuffAttack
			},
			{
				"OnMinionDebuffAttack",
				this.OnMinionDebuffAttack
			},
			{
				"OnMinionEvade",
				this.OnMinionEvade
			},
			{
				"OnCharacterPreheal",
				this.OnCharacterPreHeal
			},
			{
				"OnCharacterHealed",
				this.OnCharacterHealed
			},
			{
				"OnCharacterPreDamage",
				this.OnCharacterPreDamage
			},
			{
				"OnCharacterDamaged",
				this.OnCharacterDamaged
			},
			{
				"OnHeroPreAttack",
				this.OnHeroPreAttack
			},
			{
				"OnHeroAttacked",
				this.OnHeroAttacked
			},
			{
				"OnHeroPreDamage",
				this.OnHeroPreDamage
			},
			{
				"OnHeroDamaged",
				this.OnHeroDamaged
			},
			{
				"OnHeroPreHeal",
				this.OnHeroPreHeal
			},
			{
				"OnHeroHealed",
				this.OnHeroHealed
			},
			{
				"OnHeroGainedArmor",
				this.OnHeroGainedArmor
			},
			{
				"OnHeroBuffAttack",
				this.OnHeroBuffAttack
			},
			{
				"OnHeroDebuffAttack",
				this.OnHeroDebuffAttack
			},
			{
				"OnHeroEvade",
				this.OnHeroEvade
			},
			{
				"OnWeaponPreEquip",
				this.OnWeaponPreEquip
			},
			{
				"OnWeaponEquipped",
				this.OnWeaponEquipped
			},
			{
				"OnInspired",
				this.OnInspired
			},
			{
				"OnManaSpent",
				this.OnManaSpent
			},
			{
				"OnTurnStart",
				this.OnTurnStart
			},
			{
				"OnTurnEnd",
				this.OnTurnEnd
			}
		};
		this.HandSubjects = new Dictionary<string, IFireable>
		{
			{
				"OnHandCardDrawn",
				this.OnHandCardDrawn
			},
			{
				"OnHandTurnStart",
				this.OnHandTurnStart
			},
			{
				"OnHandTurnEnd",
				this.OnHandTurnEnd
			}
		};
	}

	public void RemoveAll()
	{
		foreach (KeyValuePair<string, IFireable> keyValuePair in this.SelfSubjects)
		{
			if (keyValuePair.Key != "Battlecry")
			{
				keyValuePair.Value.DisposeAll();
			}
		}
		foreach (KeyValuePair<string, IFireable> keyValuePair2 in this.GlobalSubjects)
		{
			keyValuePair2.Value.DisposeAll();
		}
	}

	public bool HasBattlecry()
	{
		return this.Battlecry.Count > 0;
	}

	public bool HasDeathrattle()
	{
		return this.Deathrattle.Count > 0;
	}

	public bool HasMeditate()
	{
		return this.Meditate.Count > 0;
	}

	public bool HasInspire()
	{
		return this.OnInspired.Count > 0;
	}

	public bool HasTrigger()
	{
		foreach (IFireable fireable in this.GlobalSubjects.Values)
		{
			if (fireable.Count > 0)
			{
				return true;
			}
		}
		return this.OnPreAttack.Count > 0 || this.OnAttacked.Count > 0 || this.OnPreDamage.Count > 0 || this.OnDamaged.Count > 0 || this.OnTargetedBySpell.Count > 0;
	}

	public bool HasEnrage()
	{
		return this.OnEnraged.Count > 0;
	}

	public MinionCard Card;

	public Dictionary<string, IFireable> SelfSubjects;

	public Dictionary<string, IFireable> GlobalSubjects;

	public Dictionary<string, IFireable> HandSubjects;

	public EventHolder<Character> Battlecry = new EventHolder<Character>();

	public EventHolder<Minion> Deathrattle = new EventHolder<Minion>();

	public EventHolder<Player> Meditate = new EventHolder<Player>();

	public EventHolder<MinionPreAttackEvent> OnPreAttack = new EventHolder<MinionPreAttackEvent>();

	public EventHolder<AttackedEvent> OnAttacked = new EventHolder<AttackedEvent>();

	public EventHolder<MinionPreDamageEvent> OnPreDamage = new EventHolder<MinionPreDamageEvent>();

	public EventHolder<MinionDamagedEvent> OnDamaged = new EventHolder<MinionDamagedEvent>();

	public EventHolder<InspireEvent> OnInspired = new EventHolder<InspireEvent>();

	public EventHolder<SpellCastedEvent> OnTargetedBySpell = new EventHolder<SpellCastedEvent>();

	public EventHolder<Minion> OnEnraged = new EventHolder<Minion>();

	public EventHolder<Minion> OnDisenraged = new EventHolder<Minion>();

	public EventHolder<BaseCard> OnDrawn = new EventHolder<BaseCard>();

	public EventHolder<BaseCard> OnDiscarded = new EventHolder<BaseCard>();

	public EventHolder<object> OnGameStart = new EventHolder<object>();

	public EventHolder<CardDrawnEvent> OnCardDrawn = new EventHolder<CardDrawnEvent>();

	public EventHolder<CardDrawnEvent> OnHandCardDrawn = new EventHolder<CardDrawnEvent>();

	public EventHolder<CardDiscardedEvent> OnCardDiscarded = new EventHolder<CardDiscardedEvent>();

	public EventHolder<CardPlayedEvent> OnCardPlayed = new EventHolder<CardPlayedEvent>();

	public EventHolder<CardPlayedEvent> OnOverloadedCardPlayed = new EventHolder<CardPlayedEvent>();

	public EventHolder<CardPlayedEvent> OnBattlecryCardPlayed = new EventHolder<CardPlayedEvent>();

	public EventHolder<SpellPreCastEvent> OnSpellPreCast = new EventHolder<SpellPreCastEvent>();

	public EventHolder<SpellCastedEvent> OnSpellCasted = new EventHolder<SpellCastedEvent>();

	public EventHolder<SecretPlayedEvent> OnSecretPlayed = new EventHolder<SecretPlayedEvent>();

	public EventHolder<SecretRevealedEvent> OnSecretRevealed = new EventHolder<SecretRevealedEvent>();

	public EventHolder<MinionPlayedEvent> OnMinionPlayed = new EventHolder<MinionPlayedEvent>();

	public EventHolder<MinionSummonedEvent> OnMinionSummoned = new EventHolder<MinionSummonedEvent>();

	public EventHolder<MinionPreAttackEvent> OnMinionPreAttack = new EventHolder<MinionPreAttackEvent>();

	public EventHolder<MinionAttackedEvent> OnMinionAttacked = new EventHolder<MinionAttackedEvent>();

	public EventHolder<MinionPreDamageEvent> OnMinionPreDamage = new EventHolder<MinionPreDamageEvent>();

	public EventHolder<MinionDamagedEvent> OnMinionDamaged = new EventHolder<MinionDamagedEvent>();

	public EventHolder<MinionPreHealEvent> OnMinionPreHeal = new EventHolder<MinionPreHealEvent>();

	public EventHolder<MinionHealedEvent> OnMinionHealed = new EventHolder<MinionHealedEvent>();

	public EventHolder<MinionDiedEvent> OnMinionDied = new EventHolder<MinionDiedEvent>();

	public EventHolder<MinionPoisonedEvent> OnMinionPoisoned = new EventHolder<MinionPoisonedEvent>();

	public EventHolder<MinionFrozenEvent> OnMinionFrozen = new EventHolder<MinionFrozenEvent>();

	public EventHolder<MinionEnragedEvent> OnMinionEnraged = new EventHolder<MinionEnragedEvent>();

	public EventHolder<MinionBuffEvent> OnMinionBuffAttack = new EventHolder<MinionBuffEvent>();

	public EventHolder<MinionBuffEvent> OnMinionDebuffAttack = new EventHolder<MinionBuffEvent>();

	public EventHolder<MinionEvadeEvent> OnMinionEvade = new EventHolder<MinionEvadeEvent>();

	public EventHolder<CharacterPreHealEvent> OnCharacterPreHeal = new EventHolder<CharacterPreHealEvent>();

	public EventHolder<CharacterHealedEvent> OnCharacterHealed = new EventHolder<CharacterHealedEvent>();

	public EventHolder<CharacterPreDamageEvent> OnCharacterPreDamage = new EventHolder<CharacterPreDamageEvent>();

	public EventHolder<CharacterDamagedEvent> OnCharacterDamaged = new EventHolder<CharacterDamagedEvent>();

	public EventHolder<HeroPreAttackEvent> OnHeroPreAttack = new EventHolder<HeroPreAttackEvent>();

	public EventHolder<HeroAttackedEvent> OnHeroAttacked = new EventHolder<HeroAttackedEvent>();

	public EventHolder<HeroPreDamageEvent> OnHeroPreDamage = new EventHolder<HeroPreDamageEvent>();

	public EventHolder<HeroDamagedEvent> OnHeroDamaged = new EventHolder<HeroDamagedEvent>();

	public EventHolder<HeroPreHealEvent> OnHeroPreHeal = new EventHolder<HeroPreHealEvent>();

	public EventHolder<HeroHealedEvent> OnHeroHealed = new EventHolder<HeroHealedEvent>();

	public EventHolder<HeroGainedArmorEvent> OnHeroGainedArmor = new EventHolder<HeroGainedArmorEvent>();

	public EventHolder<HeroBuffEvent> OnHeroBuffAttack = new EventHolder<HeroBuffEvent>();

	public EventHolder<HeroBuffEvent> OnHeroDebuffAttack = new EventHolder<HeroBuffEvent>();

	public EventHolder<HeroEvadeEvent> OnHeroEvade = new EventHolder<HeroEvadeEvent>();

	public EventHolder<WeaponPreEquipEvent> OnWeaponPreEquip = new EventHolder<WeaponPreEquipEvent>();

	public EventHolder<WeaponEquippedEvent> OnWeaponEquipped = new EventHolder<WeaponEquippedEvent>();

	public EventHolder<ManaSpentEvent> OnManaSpent = new EventHolder<ManaSpentEvent>();

	public EventHolder<TurnEvent> OnTurnStart = new EventHolder<TurnEvent>();

	public EventHolder<TurnEvent> OnHandTurnStart = new EventHolder<TurnEvent>();

	public EventHolder<TurnEvent> OnTurnEnd = new EventHolder<TurnEvent>();

	public EventHolder<TurnEvent> OnHandTurnEnd = new EventHolder<TurnEvent>();
}
