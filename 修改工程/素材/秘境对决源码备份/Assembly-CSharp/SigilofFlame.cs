using System;
using System.Collections;

public class SigilofFlame : SpellCard
{
	public SigilofFlame()
	{
		this.Name = "赤焰南瓜";
		this.Description = "While this is in your hand, summon a 1/1 Ghoul for your opponent at the start of your turn.";
		this.Class = HeroClass.DemonHunter;
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
			ChargeTurnGhoul minionCard = new ChargeTurnGhoul();
			yield return this.Player.Enemy.SummonMinion(minionCard);
			if (minionCard.Minion != null)
			{
				minionCard.Minion.Mechanics.RemoveAll();
				minionCard.Minion.HasCharge = false;
			}
			minionCard = null;
		}
		yield break;
	}
}
