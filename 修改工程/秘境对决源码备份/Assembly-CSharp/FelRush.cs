using System;
using System.Collections;

public class FelRush : SpellCard
{
	public FelRush()
	{
		this.Name = "龙化秘术";
		this.Description = "Turn one of your own or enemy's entourage into a flying dragon.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		HighWarlordNajentus highWarlordNajentus = new HighWarlordNajentus
		{
			BaseCost = 5,
			BaseAttack = 5,
			BaseHealth = 5,
			CurrentHealth = 5
		};
		target.As<Minion>().TransformInto(highWarlordNajentus);
		if (highWarlordNajentus.Minion != null)
		{
			highWarlordNajentus.Minion.Mechanics.RemoveAll();
		}
		yield break;
	}

	public override bool CanCast()
	{
		return GameManager.Instance.GetAllMinions().Count > 0;
	}
}
