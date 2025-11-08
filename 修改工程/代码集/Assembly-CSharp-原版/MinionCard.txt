using System;
using System.Collections;
using System.Collections.Generic;

public abstract class MinionCard : BaseCard
{
	public int CurrentAttack
	{
		get
		{
			int num = this.BaseAttack;
			foreach (Func<int, int> func in this.AttackModifiers)
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

	public int MaxHealth
	{
		get
		{
			int num = this.BaseHealth;
			foreach (Func<int, int> func in this.HealthModifiers)
			{
				num = func(num);
				if (num <= 0)
				{
					num = 1;
				}
			}
			return num;
		}
	}

	public void InitializeMinion()
	{
		base.InitializeCard();
		this.CurrentHealth = this.BaseHealth;
	}

	public IEnumerator PlayOn(Character target)
	{
		if (this.Player.Minions.Contains(this.Minion))
		{
			yield break;
		}
		if (target == null || target.IsAlive())
		{
			if (this.Player.IsEnemy)
			{
				yield return InterfaceManager.Instance.ShowEnemyCard(this);
			}
			yield return this.Player.UseMana(base.CurrentCost);
			yield return this.Player.PlayMinion(this.Minion, target);
		}
		else
		{
			this.Player.RemoveMinionFromBoard(this.Minion);
		}
		yield break;
	}

	public void AddAttackModifier(Func<int, int> modifier)
	{
		this.AttackModifiers.Add(modifier);
		if (this.Controller != null)
		{
			this.Controller.UpdateNumbers();
		}
	}

	public void RemoveAttackModifier(Func<int, int> modifier)
	{
		if (this.AttackModifiers.Contains(modifier))
		{
			this.AttackModifiers.Remove(modifier);
		}
		if (this.Controller != null)
		{
			this.Controller.UpdateNumbers();
		}
	}

	public void AddHealthModifier(Func<int, int> modifier)
	{
		this.HealthModifiers.Add(modifier);
		if (this.CurrentHealth > this.MaxHealth)
		{
			this.CurrentHealth = this.MaxHealth;
		}
		if (this.Controller != null)
		{
			this.Controller.UpdateNumbers();
		}
	}

	public void RemoveHealthModifier(Func<int, int> modifier)
	{
		if (this.HealthModifiers.Contains(modifier))
		{
			this.HealthModifiers.Remove(modifier);
		}
		if (this.Controller != null)
		{
			this.Controller.UpdateNumbers();
		}
	}

	public int BaseAttack;

	public int BaseHealth;

	public MinionType MinionType;

	public Minion Minion;

	public int CurrentHealth;

	public List<Func<int, int>> AttackModifiers = new List<Func<int, int>>();

	public List<Func<int, int>> HealthModifiers = new List<Func<int, int>>();

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
}
