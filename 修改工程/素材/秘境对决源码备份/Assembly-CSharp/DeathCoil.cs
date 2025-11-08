using System;
using System.Collections;

public class DeathCoil : SpellCard
{
	public DeathCoil()
	{
		this.Name = "凋零缠绕";
		this.Description = "Deal 5 damage to a charactor. If it's a friendly charactor, restore it 5 health.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (target.IsEnemyOf(this.Player.Hero))
		{
			int damage = 5 + this.Player.GetSpellPower();
			InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, damage);
			yield return target.Damage(null, damage);
			yield return target.CheckDeath();
		}
		else
		{
			yield return target.Heal(5);
			yield return target.CheckDeath();
		}
		yield break;
	}
}
