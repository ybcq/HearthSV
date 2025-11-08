using System;
using System.Collections;

public class DeathPact : SpellCard
{
	public DeathPact()
	{
		this.Name = "死亡契约";
		this.Description = "Destroy a friendly minion. Restore 4 Health to your hero and draw a card.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.As<Minion>().Destroy();
		yield return this.Player.Hero.Heal(4);
		yield return this.Player.Draw(null);
		yield break;
	}
}
