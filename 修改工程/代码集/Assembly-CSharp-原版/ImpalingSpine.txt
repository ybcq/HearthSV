using System;
using System.Collections;

public class ImpalingSpine : SpellCard
{
	public ImpalingSpine()
	{
		this.Name = "Impaling Spine";
		this.Description = "Silence a minion.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
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
