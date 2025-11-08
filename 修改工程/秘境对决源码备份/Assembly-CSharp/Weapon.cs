using System;
using System.Collections;
using System.Collections.Generic;

public class Weapon
{
	public Weapon(WeaponCard card)
	{
		this.Card = card;
		this.Player = card.Player;
		this.BaseAttack = card.BaseAttack;
		this.CurrentDurability = card.MaxDurability;
		this.MaxDurability = card.MaxDurability;
		this.BaseDurability = card.BaseDurability;
		this.HasWindfury = card.HasWindfury;
		this.Mechanics = card.Mechanics;
	}

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
			foreach (Func<int, int> func2 in this.AuraAttackModifiers)
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

	public virtual void Use()
	{
		Debugger.LogWeapon(this, "used");
		this.CurrentDurability--;
		if (this.CurrentDurability <= 0)
		{
			ActionQueue.Add(new Func<IEnumerator>(this.Player.DestroyWeapon));
		}
		ActionQueue.AddVoid(new Action(GameManager.Instance.GameUpdate));
	}

	public void AddAttackModifier(Func<int, int> modifier)
	{
		this.AttackModifiers.Add(modifier);
		this.Controller.UpdateNumbers();
	}

	public void RemoveAttackModifier(Func<int, int> modifier)
	{
		if (this.AttackModifiers.Contains(modifier))
		{
			this.AttackModifiers.Remove(modifier);
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

	public Player Player;

	public WeaponCard Card;

	public Mechanics Mechanics;

	public WeaponController Controller;

	public int BaseAttack;

	protected List<Func<int, int>> AttackModifiers = new List<Func<int, int>>();

	protected List<Func<int, int>> AuraAttackModifiers = new List<Func<int, int>>();

	public int BaseDurability;

	public int MaxDurability;

	public int CurrentDurability;

	public bool HasWindfury;
}
