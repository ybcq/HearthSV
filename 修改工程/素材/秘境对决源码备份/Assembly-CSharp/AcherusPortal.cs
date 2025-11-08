using System;
using System.Collections;
using System.Linq;

public class AcherusPortal : SpellCard
{
	public AcherusPortal()
	{
		this.Name = "阿彻鲁斯传送门";
		this.Description = "Give a minion +2/-2. Summon a random 2-Cost minion.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		InterfaceManager.Instance.SpawnHealSplatOn(target.Controller, 2);
		minion.AddAttackModifier(new Func<int, int>(this.AcherusPortalAttackModifier));
		minion.CurrentHealth -= 2;
		minion.AddHealthModifier(new Func<int, int>(this.AcherusPortalHealthModifier));
		MinionCard minionCard = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
		where m.BaseCost == 2
		select m).ToList<MinionCard>());
		yield return this.Player.SummonMinion(minionCard);
		yield return target.CheckDeath();
		yield break;
	}

	public int AcherusPortalAttackModifier(int attack)
	{
		return attack + 2;
	}

	public int AcherusPortalHealthModifier(int health)
	{
		return health - 2;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Count > 0;
	}
}
