using System;
using System.Collections;

public class UnholyPresence : SpellCard
{
	public UnholyPresence()
	{
		this.Name = "邪恶灾祸";
		this.Description = "Whenever you play a Deathrattle card, draw a card.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		this.Collectible = false;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.SetPresence(EventManager.Instance.CardPlayedHandler.Add(new Func<CardPlayedEvent, IEnumerator>(this.OnCardPlayed)), Presence.Unholy);
		yield break;
	}

	public IEnumerator OnCardPlayed(CardPlayedEvent evt)
	{
		if (evt.Player == this.Player && evt.Card.Description.Contains("Deathrattle"))
		{
			if (this.Player.IsSelf())
			{
				yield return InterfaceManager.Instance.ShowFriendlyCard(this);
			}
			else
			{
				yield return InterfaceManager.Instance.ShowEnemyCard(this);
			}
			yield return this.Player.Draw(null);
		}
		yield break;
	}
}
