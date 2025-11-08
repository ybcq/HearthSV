using System;
using System.Collections;
using System.Linq;

public class Asphyxiate : SpellCard
{
	public Asphyxiate()
	{
		this.Name = "窒息";
		this.Description = "Silence a minion. If it was already Silenced, destroy it instead.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		if (minion.IsSilenced)
		{
			yield return minion.Destroy();
		}
		else
		{
			minion.Silence();
		}
		yield break;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Count((Minion m) => m.Card.MinionType != MinionType.Totem) > 0;
	}
}
