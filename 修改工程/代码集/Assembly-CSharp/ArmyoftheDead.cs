using System;
using System.Collections;

public class ArmyoftheDead : SpellCard
{
	public ArmyoftheDead()
	{
		this.Name = "亡者大军";
		this.Description = "Summon seven 3/3 Ghouls with Taunt.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 10;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count < 7;
	}

	public override IEnumerator Cast(Character target)
	{
		int num;
		for (int i = 0; i < 7; i = num + 1)
		{
			ChargeTurnGhoul minionCard = new ChargeTurnGhoul
			{
				BaseAttack = 3,
				BaseHealth = 3,
				CurrentHealth = 3
			};
			yield return this.Player.SummonMinion(minionCard);
			if (minionCard.Minion != null)
			{
				minionCard.Minion.Silence();
				minionCard.Minion.HasTaunt = true;
			}
			num = i;
			minionCard = null;
		}
		yield break;
	}
}
