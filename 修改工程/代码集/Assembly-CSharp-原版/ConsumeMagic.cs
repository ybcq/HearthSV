using System;
using System.Collections;

public class ConsumeMagic : SpellCard
{
	public ConsumeMagic()
	{
		this.Name = "Consume Magic";
		this.Description = "Counter the next spell your opponent casts.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.SpellPreCastSubscription = EventManager.Instance.SpellPreCastHandler.Add(new Func<SpellPreCastEvent, IEnumerator>(this.OnSpellPreCast));
		yield break;
	}

	public IEnumerator OnSpellPreCast(SpellPreCastEvent evt)
	{
		if (evt.Player == this.Player.Enemy)
		{
			if (this.Player.IsSelf())
			{
				yield return InterfaceManager.Instance.ShowFriendlyCard(this);
			}
			else
			{
				yield return InterfaceManager.Instance.ShowEnemyCard(this);
			}
			evt.Cancel();
			this.SpellPreCastSubscription.Dispose();
		}
		yield break;
	}

	public IDisposable SpellPreCastSubscription;
}
