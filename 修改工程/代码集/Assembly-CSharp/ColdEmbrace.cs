using System;
using System.Collections;

public class ColdEmbrace : SpellCard
{
	public ColdEmbrace()
	{
		this.Name = "寒冷拥抱";
		this.Description = "Silence a minion, then Freeze it. Give it Taunt and +4 Health.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		minion.Silence();
		minion.Freeze();
		minion.HasTaunt = true;
		minion.CurrentHealth += 4;
		minion.AddHealthModifier(new Func<int, int>(this.ColdEmbraceModifier));
		yield break;
	}

	public int ColdEmbraceModifier(int health)
	{
		return health + 4;
	}
}
