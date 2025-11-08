using System;
using System.Collections;

public class AllWillServe : SpellCard
{
	public AllWillServe()
	{
		this.Name = "众生臣服";
		this.Description = "Deal 2 damage. Summon a 1/1 Ghoul with Charge.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int damage = 2 + this.Player.GetSpellPower();
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, damage);
		yield return target.Damage(null, damage);
		yield return target.CheckDeath();
		yield return this.Player.SummonMinion(new ChargeTurnGhoul());
		yield break;
	}
}
