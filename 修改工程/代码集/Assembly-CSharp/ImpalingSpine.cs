using System;
using System.Collections;

public class ImpalingSpine : SpellCard
{
	public ImpalingSpine()
	{
		this.Name = "刺穿脊柱";
		this.Description = "Silence a minion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		target.As<Minion>().Silence();
		yield break;
	}
}
