using System;
using System.Collections;

public class Candle : SpellCard
{
	public Candle()
	{
		this.Name = "Candle";
		this.Description = "While this is in your hand, summon a 1/1 Kobold for your opponent at the start of your turn.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 1;
		this.Mechanics.OnHandTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnHandTurnStart));
		base.InitializeSpell();
	}

	public IEnumerator OnHandTurnStart(TurnEvent turnEvent)
	{
		if (turnEvent.Player == this.Player)
		{
			if (turnEvent.Player == GameManager.Instance.EnemyPlayer)
			{
				yield return InterfaceManager.Instance.ShowEnemyCard(this);
			}
			else
			{
				yield return InterfaceManager.Instance.ShowFriendlyCard(this);
			}
			yield return this.Player.Enemy.SummonMinion(new Kobold());
		}
		yield break;
	}
}
