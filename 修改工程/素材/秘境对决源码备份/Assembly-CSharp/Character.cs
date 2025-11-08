using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Character
{
	public abstract int CurrentAttack { get; }

	public abstract int MaxHealth { get; }

	public virtual void Initialize()
	{
		this.CurrentHealth = this.BaseHealth;
		this.CurrentArmor = this.BaseArmor;
		this.IsSleeping = !this.HasCharge;
	}

	public abstract IEnumerator Attack(Character target);

	public abstract IEnumerator Damage(Character attacker, int damage);

	public abstract IEnumerator Heal(int heal);

	public abstract IEnumerator CheckDeath();

	public void AddAttackModifier(Func<int, int> modifier)
	{
		int currentAttack = this.CurrentAttack;
		this.AttackModifiers.Add(modifier);
		int delta = this.CurrentAttack - currentAttack;
		if (this is Minion)
		{
			ActionQueue.Add(() => EventManager.Instance.OnMinionBuffAttack(this as Minion, delta));
		}
		else
		{
			ActionQueue.Add(() => EventManager.Instance.OnHeroBuffAttack(this as Hero, delta));
		}
		this.Controller.UpdateNumbers();
	}

	public void RemoveAttackModifier(Func<int, int> modifier)
	{
		if (this.AttackModifiers.Contains(modifier))
		{
			int currentAttack = this.CurrentAttack;
			this.AttackModifiers.Remove(modifier);
			int delta = this.CurrentAttack - currentAttack;
			if (this is Minion)
			{
				ActionQueue.Add(() => EventManager.Instance.OnMinionBuffAttack(this as Minion, delta));
			}
			else
			{
				ActionQueue.Add(() => EventManager.Instance.OnHeroBuffAttack(this as Hero, delta));
			}
		}
		this.Controller.UpdateNumbers();
	}

	public void AddAuraAttackModifier(Func<int, int> modifier)
	{
		if (!this.AuraAttackModifiers.Contains(modifier))
		{
			this.AuraAttackModifiers.Add(modifier);
		}
		this.Controller.UpdateNumbers();
	}

	public void RemoveAuraAttackModifier(Func<int, int> modifier)
	{
		if (this.AuraAttackModifiers.Contains(modifier))
		{
			this.AuraAttackModifiers.Remove(modifier);
		}
		this.Controller.UpdateNumbers();
	}

	public void AddHealthModifier(Func<int, int> modifier)
	{
		this.HealthModifiers.Add(modifier);
		if (this.CurrentHealth > this.MaxHealth)
		{
			this.CurrentHealth = this.MaxHealth;
		}
		this.Controller.UpdateNumbers();
	}

	public void RemoveHealthModifier(Func<int, int> modifier)
	{
		if (this.HealthModifiers.Contains(modifier))
		{
			this.HealthModifiers.Remove(modifier);
		}
		this.Controller.UpdateNumbers();
	}

	public void AddAuraHealthModifier(Func<int, int> modifier)
	{
		this.AddAuraHealthModifier(modifier, 0);
	}

	public void AddAuraHealthModifier(Func<int, int> modifier, int addValue)
	{
		if (!this.AuraHealthModifiers.Contains(modifier))
		{
			this.AuraHealthModifiers.Add(modifier);
			this.CurrentHealth += addValue;
			if (this.CurrentHealth <= 0)
			{
				ActionQueue.Add(new Func<IEnumerator>(this.CheckDeath));
			}
		}
		if (this.CurrentHealth > this.MaxHealth)
		{
			this.CurrentHealth = this.MaxHealth;
		}
		this.Controller.UpdateNumbers();
	}

	public void RemoveAuraHealthModifier(Func<int, int> modifier)
	{
		this.RemoveAuraHealthModifier(modifier, 0);
	}

	public void RemoveAuraHealthModifier(Func<int, int> modifier, int removeValue)
	{
		int currentHealth = this.CurrentHealth;
		if (this.AuraHealthModifiers.Contains(modifier))
		{
			this.AuraHealthModifiers.Remove(modifier);
			this.CurrentHealth += removeValue;
			if (this.CurrentHealth <= 0 && currentHealth >= 0)
			{
				this.CurrentHealth = 1;
			}
		}
		if (this.CurrentHealth > this.MaxHealth)
		{
			this.CurrentHealth = this.MaxHealth;
		}
		this.Controller.UpdateNumbers();
	}

	public void Freeze()
	{
		Debugger.LogCharacter(this, "frozen");
		SoundManager.Instance.Play("Game_Mechanic_Freeze");
		this.IsFrozen = true;
		this.UnfreezeNextTurn = false;
		GameManager.Instance.GameUpdate();
	}

	public void SetEvasion(bool value)
	{
		if (value)
		{
			Debugger.LogCharacter(this, "is now Evasive");
			this.IsEvasive = true;
		}
		else
		{
			Debugger.LogCharacter(this, "isn't Evasive anymore");
			this.IsEvasive = false;
		}
		GameManager.Instance.GameUpdate();
	}

	public virtual int GetMissingHealth()
	{
		return this.MaxHealth - this.CurrentHealth;
	}

	public virtual bool CanAttack()
	{
		return false;
	}

	public virtual bool CanAttackTo(Character target)
	{
		if (!this.IsFriendlyOf(target))
		{
			if (target.IsHero())
			{
				if (!this.CantAttackHeroes)
				{
					Hero hero = target.As<Hero>();
					if (!hero.Player.HasTauntMinions() && !hero.IsStealth && !hero.IsImmune)
					{
						return true;
					}
				}
			}
			else
			{
				Minion minion = target.As<Minion>();
				if (minion.IsStealth || minion.HasWuMian)
				{
					return false;
				}
				if (minion.HasTaunt)
				{
					return !this.CantAttackTaunt;
				}
				if (!minion.Player.HasTauntMinions())
				{
					return true;
				}
			}
		}
		return false;
	}

	public virtual bool IsAlive()
	{
		return this.CurrentHealth > 0;
	}

	public virtual bool IsHero()
	{
		return false;
	}

	public virtual bool IsMinion()
	{
		return false;
	}

	public virtual bool IsFriendlyOf(Character other)
	{
		if (this.IsHero())
		{
			if (other.IsHero())
			{
				return this == other;
			}
			return this.Player.Minions.Contains(other.As<Minion>());
		}
		else
		{
			if (other.IsHero())
			{
				return other.Player.Minions.Contains(this.As<Minion>());
			}
			return this.Player.Minions.Contains(other.As<Minion>());
		}
	}

	public virtual bool IsEnemyOf(Character other)
	{
		return !this.IsFriendlyOf(other);
	}

	public bool IsDamaged()
	{
		return this.CurrentHealth != this.MaxHealth;
	}

	public int BaseHealth;

	public int BaseAttack;

	public int BaseArmor;

	public Player Player;

	public CharacterController Controller;

	protected List<Func<int, int>> AttackModifiers = new List<Func<int, int>>();

	protected List<Func<int, int>> AuraAttackModifiers = new List<Func<int, int>>();

	public int CurrentHealth;

	protected List<Func<int, int>> HealthModifiers = new List<Func<int, int>>();

	protected List<Func<int, int>> AuraHealthModifiers = new List<Func<int, int>>();

	public int CurrentArmor;

	public bool IsSleeping = true;

	public int CurrentTurnAttacks;

	public bool CantAttack;

	public bool CantAttackTaunt;

	public bool CantAttackHeroes;

	public bool HasCharge;

	public bool HasCleave;

	public bool HasDivineShield;

	public bool HasFreeze;

	public bool HasPoison;

	public bool HasSpellshield;

	public bool HasTaunt;

	public bool HasWindfury;

	public bool IsEvasive;

	public bool IsImmune;

	public bool IsInaccurate;

	public bool IsStealth;

	public int SpellPower;

	public bool IsSilenced;

	public bool IsFrozen;

	public bool IsEnraged;

	public bool UnfreezeNextTurn;

	public bool HasEvolution;

	public bool HasWuMian;
}
