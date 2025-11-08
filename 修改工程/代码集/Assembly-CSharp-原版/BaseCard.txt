using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseCard
{
	public int CurrentCost
	{
		get
		{
			int num = this.BaseCost;
			foreach (Func<int, int> func in this.CostModifiers)
			{
				num = func(num);
				if (num < 0)
				{
					num = 0;
				}
			}
			return num;
		}
	}

	public void InitializeCard()
	{
	}

	public IEnumerator Discard()
	{
		Debugger.LogCard(this, "discarded");
		yield return this.Mechanics.OnDiscarded.Fire(this);
		yield return EventManager.Instance.OnCardDiscarded(this.Player, this);
		this.Player.DiscardedCards.Add(this);
		this.Player.RemoveCardFromHand(this);
		yield break;
	}

	public void SetOwner(Player player)
	{
		this.Player = player;
	}

	public BaseCard Copy()
	{
		BaseCard baseCard = BaseCard.CreateCard(base.GetType());
		baseCard.SetOwner(this.Player);
		return baseCard;
	}

	public void AddCostModifier(Func<int, int> modifier)
	{
		if (!this.CostModifiers.Contains(modifier))
		{
			this.CostModifiers.Add(modifier);
		}
		if (this.Controller != null)
		{
			this.Controller.UpdateNumbers();
		}
	}

	public void RemoveCostModifier(Func<int, int> modifier)
	{
		if (this.CostModifiers.Contains(modifier))
		{
			this.CostModifiers.Remove(modifier);
		}
		if (this.Controller != null)
		{
			this.Controller.UpdateNumbers();
		}
	}

	public virtual bool CanBattlecry()
	{
		return true;
	}

	public virtual bool CanBattlecryTarget(Character target)
	{
		if (target == null || (!target.IsFriendlyOf(this.Player.Hero) && target.IsStealth) || this.BattlecryType == BattlecryType.None || this.BattlecryType == BattlecryType.NoTarget)
		{
			return false;
		}
		if (target.IsHero())
		{
			if (this.Player.Hero == target)
			{
				return this.BattlecryType == BattlecryType.AllCharacters || this.BattlecryType == BattlecryType.FriendlyCharacters;
			}
			return this.BattlecryType == BattlecryType.AllCharacters || this.BattlecryType == BattlecryType.EnemyCharacters;
		}
		else
		{
			if (this.Player.Hero.IsFriendlyOf(target))
			{
				return this.BattlecryType == BattlecryType.AllCharacters || this.BattlecryType == BattlecryType.AllMinions || this.BattlecryType == BattlecryType.FriendlyMinions;
			}
			return this.BattlecryType == BattlecryType.AllCharacters || this.BattlecryType == BattlecryType.AllMinions || this.BattlecryType == BattlecryType.EnemyMinions;
		}
	}

	public virtual bool CanAddToDeck(SavedDeck deck)
	{
		return true;
	}

	public void Reveal()
	{
		this.IsRevealed = true;
		this.Controller.Reveal();
	}

	public static BaseCard CreateCard(string name)
	{
		name = name.Replace(" ", string.Empty);
		Type type = Type.GetType(name);
		if (type != null)
		{
			return (BaseCard)Activator.CreateInstance(type);
		}
		return null;
	}

	public static BaseCard CreateCard(Type type)
	{
		if (type != null)
		{
			return (BaseCard)Activator.CreateInstance(type);
		}
		return null;
	}

	public CardType GetCardType()
	{
		string name = base.GetType().BaseType.Name;
		if (name != null)
		{
			if (name == "MinionCard")
			{
				return CardType.Minion;
			}
			if (name == "SpellCard")
			{
				return CardType.Spell;
			}
			if (name == "WeaponCard")
			{
				return CardType.Weapon;
			}
		}
		return CardType.None;
	}

	public string Name;

	public string Description;

	public int BaseCost;

	public HeroClass Class;

	public CardRarity Rarity;

	public bool Golden;

	public bool Collectible = true;

	public BattlecryType BattlecryType;

	public Player Player;

	public CardController Controller;

	public Mechanics Mechanics = new Mechanics();

	public Aura<Minion> MinionAura;

	public Aura<BaseCard> CardAura;

	public Aura<BaseHeroPower> HeroPowerAura;

	public Aura<Hero> HeroAura;

	private List<Func<int, int>> CostModifiers = new List<Func<int, int>>();

	public int Overload;

	public bool Combo;

	public bool IsRevealed;

	public bool HasHeld;
}
