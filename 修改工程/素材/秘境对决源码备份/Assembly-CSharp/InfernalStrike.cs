using System;
using System.Collections;

public class InfernalStrike : SpellCard
{
	public InfernalStrike()
	{
		this.Name = "精灵的诅咒";
		this.Description = "Set an enemy minion's Health to 1.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		minion.AddHealthModifier(new Func<int, int>(this.InfernalStrikeMinionModifier));
		minion.CurrentHealth = 1;
		yield break;
	}

	public int InfernalStrikeMinionModifier(int attack)
	{
		return 1;
	}
}
