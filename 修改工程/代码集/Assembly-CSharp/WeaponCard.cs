using System;
using System.Collections;
using System.Collections.Generic;

public abstract class WeaponCard : BaseCard
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

	public int MaxDurability
	{
		get
		{
			int num = this.BaseDurability;
			foreach (Func<int, int> func in this.DurabilityModifiers)
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

	public void InitializeWeapon()
	{
		base.InitializeCard();
	}

	public IEnumerator Play(Character target)
	{
		if (!this.Player.Hand.Contains(this))
		{
			yield break;
		}
		if (target == null || target.IsAlive())
		{
			this.Player.RemoveCardFromHand(this);
			if (this.Player.IsEnemy)
			{
				yield return InterfaceManager.Instance.ShowEnemyCard(this);
			}
			yield return this.Player.UseMana(base.CurrentCost);
			yield return this.Player.EquipWeapon(this, target);
		}
		yield break;
	}

	public int BaseAttack;

	public int BaseDurability;

	public Weapon Weapon;

	public List<Func<int, int>> AttackModifiers = new List<Func<int, int>>();

	public List<Func<int, int>> DurabilityModifiers = new List<Func<int, int>>();

	public bool HasWindfury;

	public bool IsInaccurate;
}
