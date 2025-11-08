using System;
using System.Collections;

public class Misery : SpellCard
{
	public Misery()
	{
		this.Name = "Misery";
		this.Description = "Held: Whenever you draw a card, your opponent draws a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 6;
		this.Mechanics.OnHandCardDrawn.Add(new Func<CardDrawnEvent, IEnumerator>(this.OnHandCardDrawn));
		base.InitializeSpell();
	}

	public IEnumerator OnHandCardDrawn(CardDrawnEvent evt)
	{
		if (evt.Card.Player == this.Player && evt.Card != this)
		{
			yield return InterfaceManager.Instance.ShowNeutralCard(this);
			yield return this.Player.Enemy.Draw(null);
		}
		yield break;
	}
}
