using System;
using System.Collections;
using System.Linq;

public class SpiritBomb : SpellCard
{
	public SpiritBomb()
	{
		this.Name = "Spirit Bomb";
		this.Description = "Destroy a random enemy minion. If you have a Soul Fragment, destroy one and an enemy minion instead.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		this.CardAura = new Aura<BaseCard>(new Action<BaseCard>(this.ApplyAura), new Action<BaseCard>(this.RemoveAura), new Func<BaseCard, bool>(this.ApplyCondition), new Func<bool>(this.ExistCondition));
		base.InitializeSpell();
	}

	public void ApplyAura(BaseCard card)
	{
		this.TargetType = TargetType.EnemyMinions;
	}

	public void RemoveAura(BaseCard card)
	{
		this.TargetType = TargetType.NoTarget;
	}

	public bool ApplyCondition(BaseCard card)
	{
		bool result;
		if (card == this)
		{
			result = this.Player.Minions.Any((Minion m) => m.Card is SoulFragment);
		}
		else
		{
			result = false;
		}
		return result;
	}

	public bool ExistCondition()
	{
		return this.Player.Hand.Contains(this);
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.TargetType == TargetType.NoTarget)
		{
			target = RNG.RandomItemFrom<Minion>(this.Player.Enemy.Minions);
		}
		else
		{
			Minion randomFragment = RNG.RandomItemFrom<Minion>((from m in this.Player.Minions
			where m.Card is SoulFragment
			select m).ToList<Minion>());
			yield return randomFragment.Destroy();
		}
		yield return target.As<Minion>().Destroy();
		yield break;
	}
}
