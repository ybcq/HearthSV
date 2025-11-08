using System;
using System.Collections;

public class ScarletCenturion : MinionCard
{
	public ScarletCenturion()
	{
		this.Name = "僵尸咏唱家";
		this.Description = "Warcry: All opponent creatures gain -1 strength.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		this.Battlecry();
		yield break;
	}

	public void Battlecry()
	{
		foreach (Minion minion in this.Player.Enemy.Minions)
		{
			if (minion.IsAlive() && minion.Card.MinionType == MinionType.Biol)
			{
				InterfaceManager.Instance.SpawnDamageSplatOn(minion.Controller, 1);
				minion.AddAttackModifier(new Func<int, int>(this.ScarletCenturionModifier));
			}
		}
	}

	public int ScarletCenturionModifier(int attack)
	{
		return attack - 1;
	}
}
