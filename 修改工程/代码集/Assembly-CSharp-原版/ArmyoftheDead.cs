using System;
using System.Collections;

public class ArmyoftheDead : SpellCard
{
	public ArmyoftheDead()
	{
		this.Name = "Army of the Dead";
		this.Description = "Summon seven 1/1 Ghouls.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 8;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count < 7;
	}

	public override IEnumerator Cast(Character target)
	{
		for (int i = 0; i < 7; i++)
		{
			yield return this.Player.SummonMinion(new NormalGhoul());
		}
		yield break;
	}
}
