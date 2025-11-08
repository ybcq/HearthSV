using System;
using System.Collections;

public class FallenrootShadowstalker : MinionCard
{
	public FallenrootShadowstalker()
	{
		this.Name = "受诅咒的信徒";
		this.Description = "Warcry: Gain -4 / -4.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 8;
		this.BaseHealth = 7;
		this.IsStealth = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public int ShadowstalkerModifier(int attack)
	{
		return attack - 4;
	}

	public IEnumerator Battlecry(Character target)
	{
		base.AddAttackModifier(new Func<int, int>(this.ShadowstalkerModifier));
		base.AddHealthModifier(new Func<int, int>(this.ShadowstalkerModifier));
		this.CurrentHealth -= 4;
		this.Minion.CheckDeath();
		yield break;
	}
}
