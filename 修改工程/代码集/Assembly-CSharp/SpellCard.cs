using System;
using System.Collections;

public abstract class SpellCard : BaseCard
{
	public void InitializeSpell()
	{
		base.InitializeCard();
	}

	public virtual IEnumerator Cast(Character target)
	{
		return null;
	}

	public virtual bool CanCast()
	{
		return true;
	}

	public virtual bool CanTarget(Character target)
	{
		if (target == null || (!target.IsFriendlyOf(this.Player.Hero) && target.IsStealth) || target.HasSpellshield)
		{
			return false;
		}
		if (target.IsHero())
		{
			if (this.Player.Hero.IsFriendlyOf(target))
			{
				return this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.FriendlyCharacters;
			}
			return this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.EnemyCharacters;
		}
		else
		{
			if (this.Player.Hero.IsFriendlyOf(target))
			{
				return this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.AllMinions || this.TargetType == TargetType.FriendlyMinions || this.TargetType == TargetType.FriendlyCharacters;
			}
			return this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.AllMinions || this.TargetType == TargetType.EnemyMinions || this.TargetType == TargetType.EnemyCharacters;
		}
	}

	public IEnumerator PlayOn(Character target)
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
			yield return this.Player.PlaySpell(this, target);
		}
		yield break;
	}

	public TargetType TargetType;
}
