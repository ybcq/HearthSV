using System;
using System.Collections;

public class FelRush : SpellCard
{
	public FelRush()
	{
		this.Name = "Fel Rush";
		this.Description = "Whenever you summon a minion this turn, deal 3 damage to a random enemy.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.MinionSummonedSubscription = EventManager.Instance.MinionSummonedHandler.Add(new Func<MinionSummonedEvent, IEnumerator>(this.OnMinionSummoned));
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield break;
	}

	public IEnumerator OnMinionSummoned(MinionSummonedEvent evt)
	{
		Character randomTarget = RNG.RandomItemFrom<Character>(this.Player.Enemy.GetAllCharacters());
		yield return randomTarget.Damage(null, 3 + this.Player.GetSpellPower());
		yield return randomTarget.CheckDeath();
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent evt)
	{
		this.MinionSummonedSubscription.Dispose();
		this.TurnEndSubscription.Dispose();
		yield break;
	}

	public IDisposable MinionSummonedSubscription;

	public IDisposable TurnEndSubscription;
}
