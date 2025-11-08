using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseHeroPower
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
			foreach (Func<int, int> func2 in this.AuraCostModifiers)
			{
				num = func2(num);
				if (num < 0)
				{
					num = 0;
				}
			}
			return num;
		}
	}

	public void Initialize(Hero hero)
	{
		this.Hero = hero;
		this.CurrentUses = 0;
	}

	public abstract IEnumerator Use(Character target);

	public virtual IEnumerator Upgrade()
	{
		yield break;
	}

	public void AddCostModifier(Func<int, int> modifier)
	{
		if (!this.CostModifiers.Contains(modifier))
		{
			this.CostModifiers.Add(modifier);
		}
		this.Controller.UpdateNumbers();
	}

	public void RemoveCostModifier(Func<int, int> modifier)
	{
		if (this.CostModifiers.Contains(modifier))
		{
			this.CostModifiers.Remove(modifier);
		}
		this.Controller.UpdateNumbers();
	}

	public void AddAuraCostModifier(Func<int, int> modifier)
	{
		if (!this.AuraCostModifiers.Contains(modifier))
		{
			this.AuraCostModifiers.Add(modifier);
		}
		this.Controller.UpdateNumbers();
	}

	public void RemoveAuraCostModifier(Func<int, int> modifier)
	{
		if (this.AuraCostModifiers.Contains(modifier))
		{
			this.AuraCostModifiers.Remove(modifier);
		}
		this.Controller.UpdateNumbers();
	}

	public virtual bool CanUse()
	{
		return true;
	}

	public bool IsAvailable()
	{
		return this.Hero.Player.CanHeroPower && this.CurrentUses < this.MaxUses && this.CurrentCost <= this.Hero.Player.AvailableMana;
	}

	public virtual bool CanTarget(Character target)
	{
		if (target == null || target.HasSpellshield)
		{
			return false;
		}
		if (target.IsFriendlyOf(this.Hero))
		{
			if (target.IsHero())
			{
				return this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.FriendlyCharacters;
			}
			return this.TargetType == TargetType.AllMinions || this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.FriendlyCharacters || this.TargetType == TargetType.FriendlyMinions;
		}
		else
		{
			if (target.IsStealth)
			{
				return false;
			}
			if (target.IsHero())
			{
				return this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.EnemyCharacters;
			}
			return this.TargetType == TargetType.AllMinions || this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.EnemyCharacters || this.TargetType == TargetType.EnemyMinions;
		}
	}

	public string Name;

	public string Description;

	public HeroClass Class;

	public TargetType TargetType;

	public int BaseCost;

	public bool Golden;

	public Hero Hero;

	public HeroPowerController Controller;

	public int MaxUses = 1;

	public int CurrentUses;

	private List<Func<int, int>> CostModifiers = new List<Func<int, int>>();

	private List<Func<int, int>> AuraCostModifiers = new List<Func<int, int>>();

	private bool IsEnemy;
}
