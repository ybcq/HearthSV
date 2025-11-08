using System;
using System.Collections;

public class DeathsAdvance : SpellCard
{
	public DeathsAdvance()
	{
		this.Name = "Deaths Advance";
		this.Description = "Draw 3 cards. At the end of your turn, discard them";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Draw(3, new Func<BaseCard, IEnumerator>(this.AddTurnEndEffect));
		yield break;
	}

	private IEnumerator AddTurnEndEffect(BaseCard card)
	{
		card.Mechanics.OnHandTurnEnd.Add((TurnEvent evt) => this.OnHandTurnEnd(card, evt));
		yield break;
	}

	private IEnumerator OnHandTurnEnd(BaseCard card, TurnEvent turnEvent)
	{
		if (turnEvent.Player == this.Player)
		{
			yield return card.Discard();
		}
		yield break;
	}
}
