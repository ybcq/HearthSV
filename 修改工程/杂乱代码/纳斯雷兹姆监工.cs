using System;
using System.Collections;
using System.Linq;

public class RiverpawGnoll : MinionCard
{
	public RiverpawGnoll()
	{
		this.Name = "ÄÉË¹À××ÈÄ·¼à¹¤";
		this.Description = "Battlecry: Give a friendly Demon +2/+2.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 4;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public int RiverpawGnollModifier(int value)
	{
		return value + 2;
	}

	public IEnumerator Battlecry(Character target)
	{
		target.AddAttackModifier(new Func<int, int>(this.RiverpawGnollModifier));
		target.AddHealthModifier(new Func<int, int>(this.RiverpawGnollModifier));
		target.CurrentHealth += 2;
		yield return true;
		yield break;
	}

	public override bool CanBattlecry()
	{
		return GameManager.Instance.GetAllMinions().Any((Minion m) => m.Card.MinionType == MinionType.Demon && m.IsFriendlyOf(this.Player.Hero));
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && target.IsFriendlyOf(this.Player.Hero) && target.IsMinion() && target.As<Minion>().Card.MinionType == MinionType.Demon;
	}
}