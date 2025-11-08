using System;
using System.Collections;
using System.Collections.Generic;

public class Burning : SpellCard
{
	public Burning()
	{
		this.Name = "Burning";
		this.Description = "Held: At the start of your turn, deal 1 damage to your characters.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.Collectible = false;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		this.Mechanics.OnHandTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnHandTurnStart));
		base.InitializeSpell();
	}

	public IEnumerator OnHandTurnStart(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			if (this.Player.IsSelf())
			{
				yield return InterfaceManager.Instance.ShowFriendlyCard(this);
			}
			else
			{
				yield return InterfaceManager.Instance.ShowEnemyCard(this);
			}
			List<Character> characters = this.Player.GetAllCharacters();
			foreach (Character character in characters)
			{
				yield return character.Damage(null, 1 + this.Player.GetSpellPower());
			}
			foreach (Character character2 in characters)
			{
				yield return character2.CheckDeath();
			}
		}
		yield break;
	}
}
