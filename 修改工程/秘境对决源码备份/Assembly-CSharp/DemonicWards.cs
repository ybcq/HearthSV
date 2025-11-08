using System;
using System.Collections;
using System.Linq;

public class DemonicWards : SpellCard
{
	public DemonicWards()
	{
		this.Name = "守护之力";
		this.Description = "Give 1 of your minion Taunt and +1/+2.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Any<Minion>();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		minion.AddAttackModifier(new Func<int, int>(this.AttackModifier));
		minion.AddHealthModifier(new Func<int, int>(this.HealthModifier));
		minion.CurrentHealth += 2;
		minion.HasTaunt = true;
		yield break;
	}

	public int AttackModifier(int attack)
	{
		return attack + 1;
	}

	public int HealthModifier(int health)
	{
		return health + 2;
	}
}
