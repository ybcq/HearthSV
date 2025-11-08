using System;
using System.Collections;

public class Fracture : SpellCard
{
	public Fracture()
	{
		this.Name = "Fracture";
		this.Description = "Deal 6 damage to a minion. Summon two Soul Fragments.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 5;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Damage(null, 6 + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield return this.Player.SummonMinion(new SoulFragment());
		yield return this.Player.SummonMinion(new SoulFragment());
		yield break;
	}
}
