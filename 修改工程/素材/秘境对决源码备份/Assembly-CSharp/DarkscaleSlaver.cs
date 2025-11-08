using System;
using System.Collections;

public class DarkscaleSlaver : MinionCard
{
	public DarkscaleSlaver()
	{
		this.Name = "恐惧龙兽";
		this.Description = "Entry Song: Inflicts 4 damage to an enemy's entourage.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 7;
		this.BaseAttack = 4;
		this.BaseHealth = 4;
		this.BattlecryType = BattlecryType.EnemyMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, 4);
		yield return target.Damage(null, 4);
		yield return target.CheckDeath();
		yield break;
	}

	public override bool CanBattlecry()
	{
		return this.Player.Enemy.Minions.Count > 0;
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && (target.IsEnemyOf(this.Player.Hero) || !target.IsStealth) && target.IsMinion() && target.As<Minion>().Card.MinionType != MinionType.Totem;
	}
}
