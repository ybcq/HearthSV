using System;
using System.Collections;
using System.Linq;

public class AcherusPortal : SpellCard
{
	public AcherusPortal()
	{
		this.Name = "Acherus Portal";
		this.Description = "Give a minion +2/-2. Summon a random 2-Cost minion.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.AddAttackModifier(new Func<int, int>(this.AcherusPortalAttackModifier));
		targetMinion.CurrentHealth -= 2;
		targetMinion.AddHealthModifier(new Func<int, int>(this.AcherusPortalHealthModifier));
		MinionCard randomMinion = RNG.RandomItemFrom<MinionCard>((from m in CardManager.Instance.AllCards.OfType<MinionCard>()
		where m.BaseCost == 2
		select m).ToList<MinionCard>());
		yield return this.Player.SummonMinion(randomMinion);
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
}
