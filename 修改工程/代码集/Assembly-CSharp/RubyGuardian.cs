using System;
using System.Collections;

public class RubyGuardian : MinionCard
{
	public RubyGuardian()
	{
		this.Name = "红宝石守护者";
		this.Description = "Battlecry: Deal 3 damage to an enemy, or restore 6 Health to a friendly character.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 7;
		this.BaseAttack = 6;
		this.BaseHealth = 6;
		this.BattlecryType = BattlecryType.AllCharacters;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		if (target.IsEnemyOf(this.Minion))
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 3);
			yield return target.Damage(null, 3);
			yield return target.CheckDeath();
		}
		else
		{
			yield return target.Heal(6);
			yield return target.CheckDeath();
		}
		yield break;
	}
}
