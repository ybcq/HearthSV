using System;
using System.Collections;

public class BloodPresence : SpellCard
{
	public BloodPresence()
	{
		this.Name = "Blood Presence";
		this.Description = "Whenever a friendly character is healed, give your hero +1 Attack this turn.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 0;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		this.Player.SetPresence(EventManager.Instance.CharacterHealedHandler.Add(new Func<CharacterHealedEvent, IEnumerator>(this.OnCharacterHealed)), Presence.Blood);
		yield break;
	}

	public IEnumerator OnCharacterHealed(CharacterHealedEvent evt)
	{
		if (this.Player.IsCurrent() && evt.Character.IsFriendlyOf(this.Player.Hero))
		{
			if (this.Player.IsSelf())
			{
				yield return InterfaceManager.Instance.ShowFriendlyCard(this);
			}
			else
			{
				yield return InterfaceManager.Instance.ShowEnemyCard(this);
			}
			this.Player.Hero.AddAttackModifier(new Func<int, int>(this.BloodPresenceModifier));
			IDisposable turnDisposable = null;
			turnDisposable = EventManager.Instance.TurnEndHandler.Add((TurnEvent x) => this.OnTurnEnd(x, turnDisposable));
		}
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent, IDisposable disposable)
	{
		this.Player.Hero.RemoveAttackModifier(new Func<int, int>(this.BloodPresenceModifier));
		disposable.Dispose();
		yield break;
	}

	public int BloodPresenceModifier(int attack)
	{
		return attack + 1;
	}
}
