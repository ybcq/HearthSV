using System;
using System.Collections;
using System.Linq;

public class DarkscaleSlaver : MinionCard
{
	public DarkscaleSlaver()
	{
		this.Name = "Darkscale Slaver";
		this.Description = "Battlecry: Take control of a damaged enemy minion that has 4 or less Attack.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Naga;
		this.BaseCost = 5;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.EnemyMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return this.Player.Minions.Count < 7 && this.Player.Enemy.Minions.Any(new Func<Minion, bool>(this.CanBattlecryTarget));
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && target.IsEnemyOf(this.Player.Hero) && !target.IsStealth && !target.HasSpellshield && target.IsMinion() && target.IsDamaged() && target.CurrentAttack <= 4;
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.TakeControlOf(target as Minion);
		yield break;
	}
}
