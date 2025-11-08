using System;
using System.Collections;

public class ScarletConjuror : MinionCard
{
	public ScarletConjuror()
	{
		this.Name = "猩红魔术师";
		this.Description = "Battlecry: Summon two 2/2 Scarlet Conjuror.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 4;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		int conjurorPosition = this.Minion.GetPosition();
		yield return this.Player.SummonMinion(new SeaElemental
		{
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		}, conjurorPosition + 1);
		yield return this.Player.SummonMinion(new SeaElemental
		{
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		}, conjurorPosition);
		yield break;
	}
}
