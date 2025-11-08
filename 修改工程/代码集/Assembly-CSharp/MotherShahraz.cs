using System;
using System.Collections;

public class MotherShahraz : MinionCard
{
	public MotherShahraz()
	{
		this.Name = "Mother Shahraz";
		this.Description = "Cleave. Battlecry: Choose an enemy minion. Set the Health of adjacent minions to 1.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Legendary;
		this.MinionType = MinionType.Demon;
		this.BaseCost = 8;
		this.BaseAttack = 5;
		this.BaseHealth = 8;
		this.HasCleave = true;
		this.BattlecryType = BattlecryType.EnemyMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return this.Player.Enemy.Minions.Count > 0;
	}

	public IEnumerator Battlecry(Character target)
	{
		Minion other = (Minion)target;
		foreach (Minion minion in this.Player.Enemy.Minions)
		{
			if (minion.IsNextTo(other))
			{
				minion.AddHealthModifier(new Func<int, int>(this.MotherShahrazModifier));
			}
		}
		yield break;
	}

	public int MotherShahrazModifier(int health)
	{
		return 1;
	}
}
