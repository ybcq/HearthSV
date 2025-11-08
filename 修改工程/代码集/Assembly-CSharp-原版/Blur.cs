using System;
using System.Collections;

public class Blur : SpellCard
{
	public Blur()
	{
		this.Name = "Blur";
		this.Description = "Draw a card. Give your hero Evasion until your next turn.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Draw(null);
		this.Player.Hero.SetEvasion(true);
		this.TurnStartSubscription = EventManager.Instance.TurnStartHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		yield break;
	}

	public IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			this.Player.Hero.SetEvasion(false);
			this.TurnStartSubscription.Dispose();
			yield break;
		}
		yield break;
	}

	public IDisposable TurnStartSubscription;
}
