using System;
using System.Collections;

public class SoulRending : SpellCard
{
	public SoulRending()
	{
		this.Name = "Soul Rending";
		this.Description = "Whenever you deal damage this turn, restore that much Health to your hero.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.HeroDamagedSubscription = EventManager.Instance.HeroDamagedHandler.Add(new Func<HeroDamagedEvent, IEnumerator>(this.OnHeroDamaged));
		this.MinionDamagedSubscription = EventManager.Instance.MinionDamagedHandler.Add(new Func<MinionDamagedEvent, IEnumerator>(this.OnMinionDamaged));
		this.TurnEndSubscription = EventManager.Instance.TurnEndHandler.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		yield break;
	}

	public IEnumerator OnHeroDamaged(HeroDamagedEvent evt)
	{
		if (evt.Attacker == null || evt.Attacker == this.Player.Hero)
		{
			yield return this.Player.Hero.Heal(evt.DamageAmount);
		}
		yield break;
	}

	public IEnumerator OnMinionDamaged(MinionDamagedEvent evt)
	{
		if (evt.Attacker == null || evt.Attacker == this.Player.Hero)
		{
			yield return this.Player.Hero.Heal(evt.DamageAmount);
		}
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		this.HeroDamagedSubscription.Dispose();
		this.MinionDamagedSubscription.Dispose();
		this.TurnEndSubscription.Dispose();
		yield break;
	}

	public IDisposable HeroDamagedSubscription;

	public IDisposable MinionDamagedSubscription;

	public IDisposable TurnEndSubscription;
}
