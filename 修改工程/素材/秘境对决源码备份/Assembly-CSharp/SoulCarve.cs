using System;
using System.Collections;

public class SoulCarve : SpellCard
{
	public SoulCarve()
	{
		this.Name = "中级诅咒";
		this.Description = "Deal 5 damage to an enemy minions.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 6;
		this.Collectible = false;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player.Enemy).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		InterfaceManager.Instance.SpawnDamageSplatOn(targetMinion.Controller, 5 + this.Player.GetSpellPower());
		yield return targetMinion.Damage(null, 5 + this.Player.GetSpellPower());
		yield return targetMinion.CheckDeath();
		yield return this.Player.AddCardToHand(new SpiritBomb());
		yield break;
	}
}
