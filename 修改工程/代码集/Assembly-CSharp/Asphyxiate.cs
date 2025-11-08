using System;
using System.Collections;

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
		Minion targetMinion = (Minion)target;
		if (targetMinion.IsSilenced)
		{
			yield return targetMinion.Destroy();
		}
		else
		{
			targetMinion.Silence();
		}
		yield break;
	}
}
